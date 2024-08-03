namespace MyGeminiAPI.Domain;

public class CodeReviewResponse
{
    public required string Code { get; set; }
    public required List<string> PossibleBugs { get; set; }
    public required string Summary { get; set; }
    public required string Language { get; set; }
    public required Performance Performance { get; set; }
    public required string Readability { get; set; }
    public required string Scalability { get; set; }
    public required string Security { get; set; }
    public required string ErrorHandling { get; set; }
    public required string Conclusion { get; set; }
    public required string AdditionalComment { get; set; }
}

public class Performance
{
    public required string TimeComplexity { get; set; }
    public required string SpaceComplexity { get; set; }
}
