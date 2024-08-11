using Microsoft.Extensions.Options;
using MyGeminiAPI.Common.Encryption;
using MyGeminiAPI.Common.PromptMakers;
using MyGeminiAPI.Configurations;
using MyGeminiAPI.Domain;
using MyGeminiAPI.Errors;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MyGeminiAPI.Services;

public class GeminiAPIService : IGeminiAPIService
{
    private readonly HttpClient _httpClient;
    private readonly GenAIConfig _genAiConfig;

    public GeminiAPIService(HttpClient httpClient, IOptionsMonitor<GenAIConfig> optionsMonitor)
    {
        _httpClient = httpClient;
        _genAiConfig = optionsMonitor.CurrentValue;
    }

    public async Task<List<QuizQuestion>> GenerateQuiz(QuizMaker quizMaker, CancellationToken cancellationToken)
    {
        var res = new List<QuizQuestion>();
        // make prompt from req body
        var quizMakingPrompt = PromptMaker.QuizMakingPrompt(quizMaker);

        // Create request body
        var reqBody = new GeminiAPIRequest()
        {
            Contents = new List<Content>()
            {
                new Content()
                {
                    Parts = new List<Part>()
                    {
                        new Part()
                        {
                            Text = quizMakingPrompt
                        }
                    }
                }
            },
            SafetySettings = new List<SafetySetting>()
            {
                new SafetySetting()
                {
                    Category =  "HARM_CATEGORY_DANGEROUS_CONTENT",
                    Threshold =  "BLOCK_ONLY_HIGH"
                }
            },
            GenerationConfig = new GenerationConfig()
            {
                //StopSequences = new List<string>() { "Title" },
                Temperature = 0.5
            }
        };

        var requestJson = JsonSerializer.Serialize(reqBody);

        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var address = $"{_genAiConfig.GenAIBaseUrl}:generateContent?key={_genAiConfig.API_KEY}";

        var response = await _httpClient.PostAsync(address, new StringContent(requestJson, Encoding.UTF8, "application/json"), cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();

            var extractedText = ExtractTextFromStringResponse(responseString);

            string jsonString = extractedText!.TrimStart('`').Substring(4);

            string neww = jsonString.Remove(jsonString.Length - 4, 4).TrimEnd('`');

            var responseList = JsonSerializer.Deserialize<List<QuizQuestion>>(neww);

            var encryptedRes = EncryptResponse(responseList!);

            return encryptedRes!;
        }
        else
        {
            return new List<QuizQuestion>();
        }
    }

    public async Task<GenAIResponse> GenerateRecipe(Recipe recipe, CancellationToken cancellationToken)
    {
        var res = new List<QuizQuestion>();
        // make prompt from req body
        var recipeMakingPrompt = PromptMaker.RecipeMakingPrompt(recipe);

        // Create request body
        var reqBody = new GeminiAPIRequest()
        {
            Contents = new List<Content>()
            {
                new Content()
                {
                    Parts = new List<Part>()
                    {
                        new Part()
                        {
                            Text = recipeMakingPrompt
                        }
                    }
                }
            },
            SafetySettings = new List<SafetySetting>()
            {
                new SafetySetting()
                {
                    Category =  "HARM_CATEGORY_DANGEROUS_CONTENT",
                    Threshold =  "BLOCK_ONLY_HIGH"
                }
            },
            GenerationConfig = new GenerationConfig()
            {
                //StopSequences = new List<string>() { "Title" },
                Temperature = 0.5
            }
        };

        var requestJson = JsonSerializer.Serialize(reqBody);

        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var address = $"{_genAiConfig.GenAIBaseUrl}:generateContent?key={_genAiConfig.API_KEY}";

        var response = await _httpClient.PostAsync(address, new StringContent(requestJson, Encoding.UTF8, "application/json"), cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();

            var extractedText = ExtractTextFromStringResponse(responseString);

            string jsonString = extractedText!.TrimStart('`').Substring(4).TrimEnd('`');

            //string neww = jsonString.Remove(jsonString.Length - 4, 4).TrimEnd('`');

            //var responseList = JsonSerializer.Deserialize<List<RecipeItem>>(neww);

            return new GenAIResponse() { ResponseMessage = jsonString, Succeed = true };
        }
        else
        {
            throw new BadRequestException("Unable to generate your recipe.");
        }
    }

    public async Task<GenAIResponse> ReviewCode(CodeReview codeReview, CancellationToken cancellationToken)
    {
        var res = new List<QuizQuestion>();
        // make prompt from req body
        var reviewerPrompt = PromptMaker.CodeReviewerPrompt(codeReview.Code);

        // Create request body
        var reqBody = new GeminiAPIRequest()
        {
            Contents = new List<Content>()
            {
                new Content()
                {
                    Parts = new List<Part>()
                    {
                        new Part()
                        {
                            Text = reviewerPrompt
                        }
                    }
                }
            },
            SafetySettings = new List<SafetySetting>()
            {
                new SafetySetting()
                {
                    Category =  "HARM_CATEGORY_DANGEROUS_CONTENT",
                    Threshold =  "BLOCK_ONLY_HIGH"
                }
            }
        };

        var requestJson = JsonSerializer.Serialize(reqBody);

        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var address = $"{_genAiConfig.GenAIBaseUrl}:generateContent?key={_genAiConfig.API_KEY}";

        var response = await _httpClient.PostAsync(address, new StringContent(requestJson, Encoding.UTF8, "application/json"), cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();

            var extractedText = ExtractTextFromStringResponse(responseString);

            string jsonString = extractedText!.TrimStart('`').Substring(4).TrimEnd('`');

            //string neww = jsonString.Remove(jsonString.Length - 4, 4).TrimEnd('`');

            //var reviewedResult = JsonSerializer.Deserialize<CodeReviewResponse>(neww);

            //return reviewedResult!;

            return new GenAIResponse () { ResponseMessage = jsonString, Succeed = true};
        }
        else
        {
            throw new BadRequestException("Unable to review your code.");
        }
    }

    public async Task<GenAIResponse> Conversation(Conversation allChats, CancellationToken cancellationToken = default)
    {
        // Create request body
        var reqBody = new GeminiAPIRequest()
        {
            Contents = allChats.Contents,
            SafetySettings = new List<SafetySetting>()
            {
                new SafetySetting()
                {
                    Category =  "HARM_CATEGORY_DANGEROUS_CONTENT",
                    Threshold =  "BLOCK_ONLY_HIGH"
                }
            },
            GenerationConfig = new GenerationConfig()
            {
                //StopSequences = new List<string>() { "Title" },
                Temperature = 0.5
            }
        };

        var requestJson = JsonSerializer.Serialize(allChats);

        // Set request headers
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var address = $"{_genAiConfig.GenAIBaseUrl}:generateContent?key={_genAiConfig.API_KEY}";

        var response = await _httpClient.PostAsync(address, new StringContent(requestJson, Encoding.UTF8, "application/json"), cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();

            var responseText = ExtractTextFromStringResponse(responseString);

            return new GenAIResponse() { Succeed = true, ResponseMessage = responseText };
        }
        else
        {
            throw new BadRequestException("Unable to process your conversation.");
        }
    }

    private string ExtractTextFromStringResponse(string responseString)
    {
        var jsonData = JsonSerializer.Deserialize<GeminiAPIResponse>(responseString);

        if (jsonData is null)
        {
            throw new BadRequestException("Unable to process your query.");
        }

        if (jsonData.Candidates is null || jsonData.Candidates.Count is 0)
        {
            throw new BadRequestException("Unable to process your query.");
        }

        if (jsonData.Candidates[0].Content is null || jsonData.Candidates[0].Content!.Parts!.Count is 0)
        {
            throw new BadRequestException("Unable to process your query.");
        }

        return jsonData!.Candidates![0].Content!.Parts![0].Text!;
    }

    private List<QuizQuestion> EncryptResponse(List<QuizQuestion> quizQuestions)
    {
        var result = quizQuestions.Select(a => new QuizQuestion()
        {
            QuestionId = a.QuestionId,
            QuestionText = a.QuestionText,
            QuestionType = a.QuestionType,
            CategoryName = a.CategoryName,
            AllOptions = a.AllOptions,
            CorrectAnswers = a.CorrectAnswers.Select(ans => AESOperation.Encrypt(ans, _genAiConfig.Encyption_Secret!)).ToList(),
        }).ToList();

        return result;
    }
}
