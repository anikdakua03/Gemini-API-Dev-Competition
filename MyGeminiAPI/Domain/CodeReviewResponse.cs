using System.Text.Json.Serialization;

namespace MyGeminiAPI.Domain;

public class CodeReviewResponse
{
    [JsonPropertyName("code")]  public required string Code { get; set; }
     [JsonPropertyName("possibleBugs")] public required List<string> PossibleBugs { get; set; }
     [JsonPropertyName("summary")] public required string Summary { get; set; }
     [JsonPropertyName("language")] public required string Language { get; set; }
     [JsonPropertyName("performance")] public required Performance Performance { get; set; }
     [JsonPropertyName("readability")] public required string Readability { get; set; }
     [JsonPropertyName("scalability")] public required string Scalability { get; set; }
     [JsonPropertyName("security")] public required string Security { get; set; }
     [JsonPropertyName("errorHandling")] public required string ErrorHandling { get; set; }
     [JsonPropertyName("conclusion")] public required string Conclusion { get; set; }
     [JsonPropertyName("additionalComment")] public required string AdditionalComment { get; set; }
}

public class Performance
{
     [JsonPropertyName("timeComplexity")] public required string TimeComplexity { get; set; }
     [JsonPropertyName("spaceComplexity")] public required string SpaceComplexity { get; set; }
}
