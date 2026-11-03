using HIMS.Core.Domain.AIAssistant;
using HIMS.Core.Domain.Dashboard;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Services.Dashboard;
using HIMS.Services.Pharmacy;
using System.Collections.Concurrent;
using System.Net;

namespace HIMS.Services.AIAssistant
{
    public class AiAssistantService : IAiAssistantService
    {
        private const int MaxMessageLength = 2000;
        private static readonly HashSet<string> AllowedLanguages = new(StringComparer.OrdinalIgnoreCase) { "en", "hi", "mr" };
        private static readonly HashSet<string> AllowedInputModes = new(StringComparer.OrdinalIgnoreCase) { "text", "voice-transcript" };
        private static readonly HashSet<string> AllowedDashboards = new(StringComparer.OrdinalIgnoreCase) { "daily", "financial", "home", "pharmacy" };

        private static readonly ConcurrentDictionary<string, ConversationOwner> Conversations = new();
        private static readonly ConcurrentDictionary<int, List<DateTime>> UserRequestHistory = new();

        private readonly IDashboardService _dashboardService;
        private readonly IPharmacyReorderService _pharmacyReorderService;

        public AiAssistantService(IDashboardService dashboardService, IPharmacyReorderService pharmacyReorderService)
        {
            _dashboardService = dashboardService;
            _pharmacyReorderService = pharmacyReorderService;
        }

        public Task<AiAssistantServiceResult<AiAssistantCapabilitiesResult>> GetCapabilitiesAsync(int userId, int unitId, int storeId, string requestId)
        {
            if (userId <= 0)
            {
                return Task.FromResult(AiAssistantServiceResult<AiAssistantCapabilitiesResult>.Fail((int)HttpStatusCode.Unauthorized, "Authentication required.", "AI_ASSISTANT_FORBIDDEN"));
            }

            var data = new AiAssistantCapabilitiesResult
            {
                Enabled = true,
                SupportedLanguages = new List<string> { "en", "hi", "mr" },
                InputModes = new List<string> { "text", "voice-transcript" },
                Topics = new List<string> { "visits", "collections", "bed-occupancy", "pharmacy-stock" },
                MaxMessageLength = MaxMessageLength
            };

            return Task.FromResult(AiAssistantServiceResult<AiAssistantCapabilitiesResult>.Ok(data, "Capabilities loaded."));
        }

        public async Task<AiAssistantServiceResult<AiAssistantChatResult>> ChatAsync(AiAssistantChatInput input)
        {
            if (input.UserId <= 0)
            {
                return AiAssistantServiceResult<AiAssistantChatResult>.Fail((int)HttpStatusCode.Unauthorized, "Authentication required.", "AI_ASSISTANT_FORBIDDEN");
            }

            if (input.UnitId <= 0 || input.StoreId <= 0)
            {
                return AiAssistantServiceResult<AiAssistantChatResult>.Fail((int)HttpStatusCode.BadRequest, "Unit/store context is required.", "AI_ASSISTANT_INVALID_REQUEST");
            }

            var normalizedLanguage = (input.Language ?? string.Empty).Trim().ToLowerInvariant();
            if (!AllowedLanguages.Contains(normalizedLanguage))
            {
                return AiAssistantServiceResult<AiAssistantChatResult>.Fail((int)HttpStatusCode.BadRequest, "Unsupported language.", "AI_ASSISTANT_INVALID_REQUEST");
            }

            var message = (input.Message ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(message) || message.Length > MaxMessageLength)
            {
                return AiAssistantServiceResult<AiAssistantChatResult>.Fail((int)HttpStatusCode.BadRequest, "Message must be non-empty and within allowed length.", "AI_ASSISTANT_INVALID_REQUEST");
            }

            if (input.Context?.InputMode is not null && !AllowedInputModes.Contains(input.Context.InputMode))
            {
                return AiAssistantServiceResult<AiAssistantChatResult>.Fail((int)HttpStatusCode.BadRequest, "Invalid input mode.", "AI_ASSISTANT_INVALID_REQUEST");
            }

            if (input.Context?.ActiveDashboard is not null && !AllowedDashboards.Contains(input.Context.ActiveDashboard))
            {
                return AiAssistantServiceResult<AiAssistantChatResult>.Fail((int)HttpStatusCode.BadRequest, "Invalid dashboard context.", "AI_ASSISTANT_INVALID_REQUEST");
            }

            if (!CheckRateLimit(input.UserId, out var retryAfterSeconds))
            {
                return AiAssistantServiceResult<AiAssistantChatResult>.Fail((int)HttpStatusCode.TooManyRequests, "The assistant is temporarily rate limited.", "AI_ASSISTANT_RATE_LIMITED", retryAfterSeconds);
            }

            string conversationId;
            if (string.IsNullOrWhiteSpace(input.ConversationId))
            {
                conversationId = Guid.NewGuid().ToString("N");
                Conversations[conversationId] = new ConversationOwner { UserId = input.UserId, UnitId = input.UnitId, StoreId = input.StoreId };
            }
            else
            {
                conversationId = input.ConversationId.Trim();
                if (!IsValidConversationId(conversationId))
                {
                    return AiAssistantServiceResult<AiAssistantChatResult>.Fail((int)HttpStatusCode.BadRequest, "Malformed conversation identifier.", "AI_ASSISTANT_INVALID_REQUEST");
                }

                if (!Conversations.TryGetValue(conversationId, out var owner) || owner.UserId != input.UserId || owner.UnitId != input.UnitId || owner.StoreId != input.StoreId)
                {
                    return AiAssistantServiceResult<AiAssistantChatResult>.Fail((int)HttpStatusCode.NotFound, "Conversation not found in the authorized scope.", "AI_ASSISTANT_CONVERSATION_NOT_FOUND");
                }
            }

            var now = AppTime.Now;
            var startOfDay = now.Date;
            var endOfDay = startOfDay.AddDays(1);
            var source = new AiAssistantSourceResult
            {
                PeriodStart = new DateTimeOffset(startOfDay),
                PeriodEnd = new DateTimeOffset(endOfDay),
                AsOf = DateTimeOffset.UtcNow
            };

            string answer;
            string sourceLabel;

            if (IsVisitsQuestion(message, normalizedLanguage))
            {
                var countData = _dashboardService.GetOPVisitCount(new HIMS.Data.DTO.OPVisitCountRequestModel
                {
                    FromDate = startOfDay,
                    ToDate = endOfDay
                });

                answer = FormatVisitsAnswer(normalizedLanguage, countData?.TotalVisitCount ?? 0);
                sourceLabel = "Outpatient visits";
            }
            else if (IsCollectionQuestion(message, normalizedLanguage))
            {
                var financial = await _dashboardService.GetFinancialDashBoard(input.UnitId, startOfDay, endOfDay);
                decimal totalReceipts = financial.ReceiptOPIP?.Sum(x => x.Receipt) ?? 0m;
                decimal totalAdvance = financial.AdvanceOPIP?.Sum(x => x.Advance) ?? 0m;
                decimal total = totalReceipts + totalAdvance;

                answer = FormatCollectionAnswer(normalizedLanguage, total, totalReceipts, totalAdvance);
                sourceLabel = "Collection summary";
            }
            else if (IsBedOccupancyQuestion(message, normalizedLanguage))
            {
                var financial = await _dashboardService.GetFinancialDashBoard(input.UnitId, startOfDay, endOfDay);
                var bedRows = financial.BedOccupancyCountSummary ?? new List<BedOccupancyCountSummary>();
                long totalBeds = bedRows.Sum(x => x.TotalBeds);
                long occupiedBeds = bedRows.Sum(x => x.OccupiedBeds);
                decimal percentage = totalBeds > 0 ? Math.Round((decimal)occupiedBeds * 100m / totalBeds, 2) : 0m;

                answer = FormatBedAnswer(normalizedLanguage, occupiedBeds, totalBeds, percentage);
                sourceLabel = "Bed occupancy";
            }
            else if (IsPharmacyStockQuestion(message, normalizedLanguage))
            {
                var lowStockList = await _pharmacyReorderService.GetListAsync(new GridRequestModel
                {
                    First = 0,
                    Rows = 10,
                    SortField = "ItemName",
                    SortOrder = 1,
                    Filters = new List<SearchGrid>(),
                    Columns = new List<GridColumn>(),
                    ExportType = ExportType.JSON
                });

                answer = FormatStockAnswer(normalizedLanguage, lowStockList.TotalCount);
                sourceLabel = "Pharmacy stock";
            }
            else
            {
                answer = UnsupportedScopeMessage(normalizedLanguage);
                sourceLabel = "Assistant scope";
            }

            source.Label = sourceLabel;
            var result = new AiAssistantChatResult
            {
                ConversationId = conversationId,
                RequestId = string.IsNullOrWhiteSpace(input.RequestId) ? Guid.NewGuid().ToString("N") : input.RequestId,
                Reply = new AiAssistantReplyResult
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Role = "assistant",
                    Text = answer,
                    Language = normalizedLanguage,
                    CreatedAt = DateTime.UtcNow
                },
                Sources = new List<AiAssistantSourceResult> { source }
            };

            return AiAssistantServiceResult<AiAssistantChatResult>.Ok(result, "Answer generated.");
        }

        private static bool IsValidConversationId(string conversationId)
        {
            if (conversationId.Length is < 8 or > 128)
            {
                return false;
            }

            return conversationId.All(ch => char.IsLetterOrDigit(ch) || ch == '-');
        }

        private static bool CheckRateLimit(int userId, out int retryAfterSeconds)
        {
            retryAfterSeconds = 0;
            var now = DateTime.UtcNow;
            var history = UserRequestHistory.GetOrAdd(userId, _ => new List<DateTime>());

            lock (history)
            {
                history.RemoveAll(t => (now - t).TotalSeconds > 60);
                if (history.Count >= 30)
                {
                    var oldest = history.Min();
                    retryAfterSeconds = Math.Max(1, 60 - (int)(now - oldest).TotalSeconds);
                    return false;
                }

                history.Add(now);
                return true;
            }
        }

        private static bool IsVisitsQuestion(string message, string language)
        {
            return ContainsAny(message, language switch
            {
                "hi" => new[] { "रोगी", "मरीज", "आए", "आज कितने", "विजिट" },
                "mr" => new[] { "रुग्ण", "आले", "भेट", "आज किती", "व्हिजिट" },
                _ => new[] { "visit", "patient", "visited today", "today patients" }
            });
        }

        private static bool IsCollectionQuestion(string message, string language)
        {
            return ContainsAny(message, language switch
            {
                "hi" => new[] { "कलेक्शन", "संग्रह", "आज की वसूली", "राजस्व" },
                "mr" => new[] { "कलेक्शन", "वसुली", "संकलन", "महसूल" },
                _ => new[] { "collection", "revenue", "today collection", "summary" }
            });
        }

        private static bool IsBedOccupancyQuestion(string message, string language)
        {
            return ContainsAny(message, language switch
            {
                "hi" => new[] { "बेड", "ऑक्युपेंसी", "अधिभोग", "खाली", "भरे" },
                "mr" => new[] { "बेड", "ऑक्युपन्सी", "व्याप्ती", "रिक्त", "भरले" },
                _ => new[] { "bed", "occupancy", "occupied", "available" }
            });
        }

        private static bool IsPharmacyStockQuestion(string message, string language)
        {
            return ContainsAny(message, language switch
            {
                "hi" => new[] { "फार्मेसी", "स्टॉक", "लो स्टॉक", "कम स्टॉक", "रीऑर्डर" },
                "mr" => new[] { "फार्मसी", "स्टॉक", "कमी स्टॉक", "लो स्टॉक", "रीऑर्डर" },
                _ => new[] { "pharmacy", "stock", "low stock", "reorder" }
            });
        }

        private static bool ContainsAny(string message, IEnumerable<string> words)
        {
            var m = message.ToLowerInvariant();
            return words.Any(w => m.Contains(w.ToLowerInvariant()));
        }

        private static string FormatVisitsAnswer(string language, int totalVisitCount)
        {
            return language switch
            {
                "hi" => $"आज कुल {totalVisitCount} विजिट दर्ज हुई हैं।",
                "mr" => $"आज एकूण {totalVisitCount} भेटी नोंदल्या गेल्या आहेत.",
                _ => $"A total of {totalVisitCount} visits were recorded today."
            };
        }

        private static string FormatCollectionAnswer(string language, decimal total, decimal receipts, decimal advance)
        {
            return language switch
            {
                "hi" => $"आज की कुल कलेक्शन राशि {total:0.00} है (रसीद {receipts:0.00}, एडवांस {advance:0.00}).",
                "mr" => $"आजची एकूण वसुली {total:0.00} आहे (रसीद {receipts:0.00}, अॅडव्हान्स {advance:0.00}).",
                _ => $"Today's total collection is {total:0.00} (receipts {receipts:0.00}, advances {advance:0.00})."
            };
        }

        private static string FormatBedAnswer(string language, long occupiedBeds, long totalBeds, decimal percentage)
        {
            return language switch
            {
                "hi" => $"वर्तमान बेड ऑक्युपेंसी {occupiedBeds}/{totalBeds} ({percentage:0.##}%) है।",
                "mr" => $"सध्याची बेड ऑक्युपन्सी {occupiedBeds}/{totalBeds} ({percentage:0.##}%) आहे.",
                _ => $"Current bed occupancy is {occupiedBeds}/{totalBeds} ({percentage:0.##}%)."
            };
        }

        private static string FormatStockAnswer(string language, int lowStockCount)
        {
            return language switch
            {
                "hi" => $"कम स्टॉक में {lowStockCount} फार्मेसी आइटम हैं।",
                "mr" => $"कमी स्टॉकमध्ये {lowStockCount} फार्मसी आयटम आहेत.",
                _ => $"There are {lowStockCount} pharmacy items in low stock."
            };
        }

        private static string UnsupportedScopeMessage(string language)
        {
            return language switch
            {
                "hi" => "अभी मैं केवल विजिट, कलेक्शन, बेड ऑक्युपेंसी और फार्मेसी लो-स्टॉक से जुड़े डैशबोर्ड प्रश्नों का उत्तर दे सकता हूँ।",
                "mr" => "सध्या मी फक्त भेटी, वसुली, बेड ऑक्युपन्सी आणि फार्मसी कमी-स्टॉक या डॅशबोर्ड प्रश्नांनाच उत्तर देऊ शकतो.",
                _ => "I can currently answer only dashboard questions on visits, collections, bed occupancy, and pharmacy low stock."
            };
        }

        private class ConversationOwner
        {
            public int UserId { get; set; }
            public int UnitId { get; set; }
            public int StoreId { get; set; }
        }
    }
}
