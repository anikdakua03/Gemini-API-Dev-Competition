namespace MyGeminiAPI.Domain; 

public class Recipe
{
    public required List<string> Ingredients { get; set; }
    public required bool IsVeg { get; set; }
    public required string DishRegion { get; set; }
}
