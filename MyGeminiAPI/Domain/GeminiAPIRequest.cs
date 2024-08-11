using System.Text.Json.Serialization;

namespace MyGeminiAPI.Domain;

public class GeminiAPIRequest
{
    [JsonPropertyName("contents"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Content>? Contents { get; set; }

    [JsonPropertyName("safetySettings"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<SafetySetting>? SafetySettings { get; set; }

    [JsonPropertyName("generationConfig"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public GenerationConfig? GenerationConfig { get; set; }
}

public class SafetySetting
{
    [JsonPropertyName("category"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Category { get; set; } = "HARM_CATEGORY_DANGEROUS_CONTENT";

    [JsonPropertyName("threshold"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Threshold { get; set; } = "BLOCK_MEDIUM_AND_ABOVE";
}

public class GenerationConfig
{
    [JsonPropertyName("stopSequences")]
    public List<string>? StopSequences { get; set; }

    [JsonPropertyName("temperature")]
    public double Temperature { get; set; } = 0.0;

    //[JsonPropertyName("maxOutputTokens")]
    //public int MaxOutputTokens { get; set; }

    //[JsonPropertyName("topP")]
    //public double TopP { get; set; }

    //[JsonPropertyName("topK")]
    //public int TopK { get; set; }
}