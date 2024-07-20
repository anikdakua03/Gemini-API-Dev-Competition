using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MyGeminiAPI.Domain;
using MyGeminiAPI.Services;
using System.Text;
using System.Text.Json;

namespace MyGeminiAPI.Controllers
{
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

        [HttpGet("qsList")]
        public async Task<IActionResult> GenerateQuizQss(string text)
        {
            string jsonString = text.TrimStart('`').TrimEnd('`').Substring(4);

            var responseList = JsonSerializer.Deserialize<List<QuizQuestion>>(jsonString);

            return Ok(responseList);
        }
    }
}
