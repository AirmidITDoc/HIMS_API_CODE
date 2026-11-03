namespace HIMS.Core.Domain.AIAssistant
{
    public class AiAssistantChatInput
    {
        public int UserId { get; set; }
        public int UnitId { get; set; }
        public int StoreId { get; set; }
        public string RequestId { get; set; } = string.Empty;
        public string? ConversationId { get; set; }
        public string? Message { get; set; }
        public string? Language { get; set; }
        public AiAssistantInputContext? Context { get; set; }
    }

    public class AiAssistantInputContext
    {
        public string? Source { get; set; }
        public string? ActiveDashboard { get; set; }
        public string? InputMode { get; set; }
    }

    public class AiAssistantCapabilitiesResult
    {
        public bool Enabled { get; set; }
        public List<string> SupportedLanguages { get; set; } = new();
        public List<string> InputModes { get; set; } = new();
        public List<string> Topics { get; set; } = new();
        public int MaxMessageLength { get; set; }
    }

    public class AiAssistantChatResult
    {
        public string ConversationId { get; set; } = string.Empty;
        public AiAssistantReplyResult Reply { get; set; } = new();
        public List<AiAssistantSourceResult> Sources { get; set; } = new();
        public string RequestId { get; set; } = string.Empty;
    }

    public class AiAssistantReplyResult
    {
        public string Id { get; set; } = string.Empty;
        public string Role { get; set; } = "assistant";
        public string Text { get; set; } = string.Empty;
        public string Language { get; set; } = "en";
        public DateTime CreatedAt { get; set; }
    }

    public class AiAssistantSourceResult
    {
        public string Label { get; set; } = string.Empty;
        public DateTimeOffset? PeriodStart { get; set; }
        public DateTimeOffset? PeriodEnd { get; set; }
        public DateTimeOffset AsOf { get; set; }
    }

    public class AiAssistantServiceResult<T>
    {
        public bool Success { get; set; }
        public int HttpStatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? ErrorCode { get; set; }
        public int? RetryAfterSeconds { get; set; }
        public T? Data { get; set; }

        public static AiAssistantServiceResult<T> Ok(T data, string message)
        {
            return new AiAssistantServiceResult<T>
            {
                Success = true,
                HttpStatusCode = 200,
                Message = message,
                Data = data
            };
        }

        public static AiAssistantServiceResult<T> Fail(int httpStatusCode, string message, string errorCode, int? retryAfterSeconds = null)
        {
            return new AiAssistantServiceResult<T>
            {
                Success = false,
                HttpStatusCode = httpStatusCode,
                Message = message,
                ErrorCode = errorCode,
                RetryAfterSeconds = retryAfterSeconds
            };
        }
    }
}
