using MyGeminiAPI.Domain;

namespace MyGeminiAPI.Common.PromptMakers
{
    public static class PromptMaker
    {
        public static string QuizMakingPrompt(QuizMaker quizMaker)
        {
            string quizMakerPrompt = $"Generate {quizMaker.QuestionCount} quiz questions based on {quizMaker.CategoryName} category , question type will be {quizMaker.QuestionType} , all question difficulty level will be {quizMaker.QuestionLevel}. The output must be a simple string format from where I can deserialize into a object in C# and each object must contain 'QuestionId' , will be integer, 'QuestionText' for actual question text, 'QuestionType' for that question type must be of 'Boolean' or 'Multiple' or 'Only_One' or mixture of these three type for 'Any' type, 'CategoryName' for that questions category, 'AllOptions' for giving options including correct option also and it will be string array, 'CorrectAnswers' for correct answers which must be from 'allOptions' array and it will be string array. ";

            return quizMakerPrompt;
        }
    }
}
