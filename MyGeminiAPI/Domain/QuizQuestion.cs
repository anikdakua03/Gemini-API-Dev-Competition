using System.Text.Json.Serialization;

namespace MyGeminiAPI.Domain; 

public class QuizQuestion
{
    [JsonPropertyName("questionId")] public int QuestionId { get; set; }
    [JsonPropertyName("questionText")] public required string QuestionText { get; set; }
    [JsonPropertyName("questionType")] public required string QuestionType { get; set; }
    // public required string QuestionLevel { get; set; }
    [JsonPropertyName("categoryName")] public required string CategoryName { get; set; }
    [JsonPropertyName("allOptions")] public required List<string> AllOptions { get; set; }
    [JsonPropertyName("correctAnswers")] public required List<string> CorrectAnswers { get; set; }
}
