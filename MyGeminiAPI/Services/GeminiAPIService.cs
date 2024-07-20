using Microsoft.Extensions.Options;
using MyGeminiAPI.Common.Encryption;
using MyGeminiAPI.Common.PromptMakers;
using MyGeminiAPI.Configurations;
using MyGeminiAPI.Domain;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MyGeminiAPI.Services
{
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
            // Create JSON request content
            var requestContent = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new
                            {
                                text = quizMakingPrompt
                            }
                        }
                    }
                }
            };

            var requestJson = JsonSerializer.Serialize(requestContent);

            // Set request headers
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Send POST request with error handling
            var address = $"{_genAiConfig.GenAIBaseUrl}:generateContent?key={_genAiConfig.API_KEY}";

            var response = await _httpClient.PostAsync(address, new StringContent(requestJson, Encoding.UTF8, "application/json"), cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadFromJsonAsync<GeminiAPIResponse>();

                var extractedText = responseJson!.candidates![0].content!.parts![0].text;

                string jsonString = extractedText!.TrimStart('`').Substring(4);
                string neww = jsonString.Remove(jsonString.Length - 4 , 4).TrimEnd('`');

                // should be like this after removing extra
                //string jsonString = "\r\n[\r\n  {\r\n    \"QuestionId\": 1,\r\n    \"QuestionText\": \"Which team holds the record for the most World Cup wins?\",\r\n    \"QuestionType\": \"Only_One\",\r\n    \"CategoryName\": \"Cricket\",\r\n    \"AllOptions\": [\r\n      \"Australia\",\r\n      \"India\",\r\n      \"West Indies\",\r\n      \"Sri Lanka\"\r\n    ],\r\n    \"CorrectAnswers\": [\r\n      \"Australia\"\r\n    ]\r\n  },\r\n  {\r\n    \"QuestionId\": 2,\r\n    \"QuestionText\": \"Is a 'googly' a type of delivery bowled by a right-arm spinner?\",\r\n    \"QuestionType\": \"Boolean\",\r\n    \"CategoryName\": \"Cricket\",\r\n    \"AllOptions\": [\r\n      \"True\",\r\n      \"False\"\r\n    ],\r\n    \"CorrectAnswers\": [\r\n      \"False\"\r\n    ]\r\n  }\r\n]\r\n";

                // getting like this
                //"```json\r\n[\r\n  {\r\n    \"QuestionId\": 1,\r\n    \"QuestionText\": \"Which team has won the most Cricket World Cups?\",\r\n    \"QuestionType\": \"Only_One\",\r\n    \"CategoryName\": \"Cricket\",\r\n    \"AllOptions\": [\r\n      \"Australia\",\r\n      \"India\",\r\n      \"West Indies\",\r\n      \"England\"\r\n    ],\r\n    \"CorrectAnswers\": [\r\n      \"Australia\"\r\n    ]\r\n  },\r\n  {\r\n    \"QuestionId\": 2,\r\n    \"QuestionText\": \"Is a 'googly' a type of delivery bowled by a right-arm bowler?\",\r\n    \"QuestionType\": \"Boolean\",\r\n    \"CategoryName\": \"Cricket\",\r\n    \"AllOptions\": [\r\n      \"True\",\r\n      \"False\"\r\n    ],\r\n    \"CorrectAnswers\": [\r\n      \"False\"\r\n    ]\r\n  }\r\n]\r\n```\r\n"

                var responseList = JsonSerializer.Deserialize<List<QuizQuestion>>(neww);

                var encryptedRes = EncryptResponse(responseList!);

                return encryptedRes!;
        }
            else
            {
                return res;
            }
}

        private string ExtractTextFromStringResponse(string responseString)
        {
            var jsonData = JsonSerializer.Deserialize<GeminiAPIResponse>(responseString)!;

            return jsonData!.candidates![0].content!.parts![0].text!;
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
}
