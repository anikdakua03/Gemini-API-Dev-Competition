using MyGeminiAPI.Domain;

namespace MyGeminiAPI.Common.PromptMakers;

public static class PromptMaker
{
    public static string QuizMakingPrompt(QuizMaker quizMaker)
    {
        string quizMakerPrompt = $"Follow the input instructions given below and generate a list of questions. " +
            $"The input instructions are :" +
            $"- 'No of questions : ```{quizMaker.QuestionCount}```' , " +
            $"- 'Category : ```{quizMaker.CategoryName}```', " +
            $"- 'Type : ```{quizMaker.QuestionType}```', " +
            $"- 'Level : ```{quizMaker.QuestionLevel}```', " +

            $"The inputs are delimited with triple backticks." +

            $"The JSON structure of the question will consist these following keys -" +
            $"'QuestionId' as integer, " +
            $"'QuestionText' of string type," +
            $"'QuestionType' must be one of these type 'Boolean' or 'Multiple' or 'Only_One' or 'Any', " +
            $"'CategoryName' of string type, " +
            $"'AllOptions' as an array,  " +
            $"'CorrectAnswers' as an array , these correct answer or answers must be included in the 'AllOptions' array,";

        return quizMakerPrompt;
    }

    public static string RecipeMakingPrompt(Recipe recipe)
    {
        string recipeMakerPrompt = $"Generate maximum of 10 and minimum of 1 recipes or dish or cuisine which must be include following ingredients." +
        $"The some of the following constarints are " +
        $"- 'Ingredients : ```{recipe.Ingredients}```' , " +
        $"- 'Is it vegeterain : ```{recipe.IsVeg}```', " +
        $"- 'Cuisine Type : ```{recipe.DishRegion}```', " +
        $"The inputs are delimited with triple backticks." +
        $"Format your response as a JSON object with -" +
        $"'Title' of string type, " +
        $"'ShortDescription' of string type," +
        $"'DishRegion' of string type, " +
        $"'IsVeg' of boolean type, " +
        $"'Ingredients' of array of strings,  " +
        $"'Instruction' and it should be of rich text format with all types of markdown styling of string type where all the points can be rendered and should have structure like following format:" +
            $"Step 1 : STEP DETAILS..." +
            $"Step 2 : STEP DETAILS.." +
            $"Step N : STEP DETAILS.., all the steps will be in the new line" +
        $"'Summary' of string type," +
        $"All the recipes must not contain any types of Cow meat or Pork.";

        return recipeMakerPrompt;
    }

    public static string CodeReviewerPrompt(string code)
    {
        string codeReviewerPrompt = $"Review the following items from the code or query. " +
            $"The following items are " +
            $"- 'PossibleBugs' , " +
            $"- 'Summary', " +
            $"- 'Language', " +
            $"- 'Performance', " +
            $"- 'Readability', " +
            $"- 'Scalability', " +
            $"- 'Security', " +
            $"- 'ErrorHandling', " +
            $"- 'Conclusion' and " +
            $"- 'AdditionalComment'." +
            $"The code or query is delimited with triple backticks." +
            $"Format your response as a JSON object with -" +
            $"'Code' of string type, " +
            $"'PossibleBugs', it will be a array of strings, " +
            $"'Summary' of string type, " +
            $"'Language' of string type, " +
            $"'Performance' object will have two keys 'TimeComplexity' and 'SpaceComplexity',  " +
            $"'Readability' of string type," +
            $"'Scalability' of string type," +
            $"'Security' of string type," +
            $"'ErrorHandling' of string type" +
            $"'Conclusion' of string type" +
            $"'AdditionalComment' of string type and this can the corrected code snippet." +
            $"The code or query : ```{code}```" +
            $"If there is no code provided , then you can just provide the code based on requested query.";

        return codeReviewerPrompt;
    }
}
