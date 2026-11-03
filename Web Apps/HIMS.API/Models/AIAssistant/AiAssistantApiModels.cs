namespace HIMS.API.Models.AIAssistant
{
    public class AiAssistantChatRequest
    {
        public string? ConversationId { get; set; }
        public string? Message { get; set; }
        public string? Language { get; set; }
        public AiAssistantRequestContext? Context { get; set; }
    }

    public class AiAssistantRequestContext
    {
        public string? Source { get; set; }
        public string? ActiveDashboard { get; set; }
        public string? InputMode { get; set; }
    }

    public class AiAssistantCapabilitiesResponse
    {
        public bool Enabled { get; set; }
        public List<string> SupportedLanguages { get; set; } = new();
        public List<string> InputModes { get; set; } = new();
        public List<string> Topics { get; set; } = new();
        public int MaxMessageLength { get; set; }
    }

    public class AiAssistantChatResponse
    {
        public string ConversationId { get; set; } = string.Empty;
        public AiAssistantReply Reply { get; set; } = new();
        public List<AiAssistantSource> Sources { get; set; } = new();
        public string RequestId { get; set; } = string.Empty;
    }

    public class AiAssistantReply
    {
        public string Id { get; set; } = string.Empty;
        public string Role { get; set; } = "assistant";
        public string Text { get; set; } = string.Empty;
        public string Language { get; set; } = "en";
        public DateTime CreatedAt { get; set; }
    }

    public class AiAssistantSource
    {
        public string Label { get; set; } = string.Empty;
        public DateTimeOffset? PeriodStart { get; set; }
        public DateTimeOffset? PeriodEnd { get; set; }
        public DateTimeOffset AsOf { get; set; }
    }

    public class AiAssistantErrorData
    {
        public string Code { get; set; } = string.Empty;
        public string RequestId { get; set; } = string.Empty;
        public int? RetryAfterSeconds { get; set; }
    }
}
