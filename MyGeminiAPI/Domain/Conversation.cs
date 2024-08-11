using System.Text.Json.Serialization;

namespace MyGeminiAPI.Domain;

public class Conversation
{
    [JsonPropertyName("contents")]
    public List<Content>? Contents { get; set; }
}
