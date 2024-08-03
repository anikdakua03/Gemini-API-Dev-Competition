using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MyGeminiAPI.Domain;
using MyGeminiAPI.Errors;
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
        if(!ModelState.IsValid)
        {
            throw new BadRequestException("Quiz making form is not valid.");
        }

        var res = await _geminiAPIService.GenerateQuiz(quizMaker, HttpContext.RequestAborted);

        return Ok(res);
    }

    [HttpPost("generate-recipe")]
    public async Task<IActionResult> GenerateRecipe(Recipe recipe)
    {
        if (!ModelState.IsValid)
        {
            throw new BadRequestException("Recipe making form is not valid.");
        }

        var res = await _geminiAPIService.GenerateRecipe(recipe, HttpContext.RequestAborted);

        return Ok(res);
    }

    [HttpPost("review-code")]
    public async Task<IActionResult> CodeReviewer(CodeReview codeReview)
    {
        if (!ModelState.IsValid)
        {
            throw new BadRequestException("Code review form is not valid.");
        }

        var res = await _geminiAPIService.ReviewCode(codeReview, HttpContext.RequestAborted);

        return Ok(res);
    }

    [HttpPost("conversation")]
    public async Task<IActionResult> Conversation(Conversation conversation)
    {
        if (!ModelState.IsValid)
        {
            throw new BadRequestException("Conversation is not valid.");
        }

        var res = await _geminiAPIService.Conversation(conversation, HttpContext.RequestAborted);

        return Ok(res);
    }
}
