using MyGeminiAPI.Domain;

namespace MyGeminiAPI.Services; 

public interface IGeminiAPIService
{
    public Task<List<QuizQuestion>> GenerateQuiz(QuizMaker quizMaker, CancellationToken cancellationToken);

    public Task<List<RecipeItem>> GenerateRecipe(Recipe recipe, CancellationToken cancellationToken);
}
