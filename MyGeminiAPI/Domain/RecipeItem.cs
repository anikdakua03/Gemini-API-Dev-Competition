namespace MyGeminiAPI.Domain; 

public class RecipeItem
{
    public required string Title { get; set; } 
    public required string  ShortDescription { get; set; }
    public required string DishRegion { get; set; }
    public required bool IsVeg { get; set; }
    public required List<string> Ingredients { get; set; }
    public required string Instructions { get; set; }
    public required string Summary { get; set; }
}
