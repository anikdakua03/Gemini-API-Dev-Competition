using System.Text.Json.Serialization;

namespace MyGeminiAPI.Domain; 

public class GeminiAPIResponse
{
    [JsonPropertyName("candidates")]
    public List<Candidate>? Candidates { get; set; }

    [JsonPropertyName("promptFeedback")]
    public PromptFeedback? PromptFeedback { get; set; }

    [JsonPropertyName("usageMetadata")]
    public UsageMetadata? UsageMetadata { get; set; }
}

public class UsageMetadata
{
    [JsonPropertyName("promptTokenCount")]
    public int PromptTokenCount { get; set; }

    [JsonPropertyName("candidatesTokenCount")]
    public int CandidatesTokenCount { get; set; }

    [JsonPropertyName("totalTokenCount")]
    public int TotalTokenCount { get; set; }
}

public class Candidate
{
    [JsonPropertyName("content")]
    public Content? Content { get; set; }

    [JsonPropertyName("finishReason")]
    public string? FinishReason { get; set; }

    [JsonPropertyName("index")]
    public int Index { get; set; }

    [JsonPropertyName("safetyRatings")]
    public List<SafetyRating>? SafetyRatings { get; set; }
}

public class Content
{
    [JsonPropertyName("role")]
    public string? Role { get; set; }

    [JsonPropertyName("parts")]
    public List<Part>? Parts { get; set; }
}

public class Part
{
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("inline_data")]
    public InlineData? InlineData { get; set; }
}

public class SafetyRating
{
    [JsonPropertyName("category")]
    public string? Category { get; set; }

    [JsonPropertyName("probability")]
    public string? Probability { get; set; }
}

public class PromptFeedback
{
    [JsonPropertyName("safetyRatings")]
    public List<SafetyRating>? SafetyRatings { get; set; }
}

public class InlineData
{
    [JsonPropertyName("mime_type"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? MimeType { get; set; }

    [JsonPropertyName("data"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Data { get; set; }
}