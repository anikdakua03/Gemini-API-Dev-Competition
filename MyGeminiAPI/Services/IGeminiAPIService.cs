using MyGeminiAPI.Domain;

namespace MyGeminiAPI.Services; 

public interface IGeminiAPIService
{
    public Task<List<QuizQuestion>> GenerateQuiz(QuizMaker quizMaker, CancellationToken cancellationToken);

    public Task<GenAIResponse> GenerateRecipe(Recipe recipe, CancellationToken cancellationToken);

    public Task<GenAIResponse> ReviewCode(CodeReview codeReview, CancellationToken cancellationToken);

    public Task<GenAIResponse> Conversation(Conversation conversation, CancellationToken cancellationToken = default);
}
