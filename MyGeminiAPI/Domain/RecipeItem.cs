using System.Text.Json.Serialization;

namespace MyGeminiAPI.Domain; 

public class RecipeItem
{
    [JsonPropertyName("title")] public required string Title { get; set; } 
    [JsonPropertyName("shortDescription")] public required string  ShortDescription { get; set; }
    [JsonPropertyName("dishRegion")] public required string DishRegion { get; set; }
    [JsonPropertyName("isVeg")] public required bool IsVeg { get; set; }
    [JsonPropertyName("ingredients")] public required List<string> Ingredients { get; set; }
    [JsonPropertyName("instruction")] public required string Instruction { get; set; }
    [JsonPropertyName("summary")] public required string Summary { get; set; }
}
