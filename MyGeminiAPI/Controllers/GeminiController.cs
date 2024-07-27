using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MyGeminiAPI.Domain;
using MyGeminiAPI.Services;

namespace MyGeminiAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting("fixed")]
public class GeminiController : ControllerBase
{
    private readonly IGeminiAPIService _geminiAPIService;

    public GeminiController(IGeminiAPIService geminiAPIService)
    {
        _geminiAPIService = geminiAPIService;
    }

    [HttpPost("generate-quiz")]
    public async Task<IActionResult> GenerateQuiz(QuizMaker quizMaker)
    {
        var res = await _geminiAPIService.GenerateQuiz(quizMaker, HttpContext.RequestAborted);

        return Ok(res);
    }

    [HttpPost("generate-recipe")]
    public async Task<IActionResult> GenerateRecipe(Recipe recipe)
    {
        var res = await _geminiAPIService.GenerateRecipe(recipe, HttpContext.RequestAborted);

        return Ok(res);
    }
}
