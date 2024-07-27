namespace MyGeminiAPI.Domain; 

public class QuizMaker
{
    public int QuestionCount { get; set; } = 10;
    public string CategoryName { get; set; } = "random";
    public string QuestionType { get; set; } = "Single option correct";
    public required string QuestionLevel { get; set; } = "Medium";
}
