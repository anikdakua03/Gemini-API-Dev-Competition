using System.Text.Json.Serialization;

namespace MyGeminiAPI.Domain; 

public class QuizQuestion
{
    public int QuestionId { get; set; }
    public required string QuestionText { get; set; }
    public required string QuestionType { get; set; }
    // public required string QuestionLevel { get; set; }
    public required string CategoryName { get; set; }
    public required List<string> AllOptions { get; set; }
    public required List<string> CorrectAnswers { get; set; }
}
