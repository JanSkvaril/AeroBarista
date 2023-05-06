using AeroBarista.Enums;

namespace AeroBarista.Models;

[Serializable]
public record RecipeModel(
    int Id,
    string Name,
    string? Description,
    RecipeMethod Method,
    RecipeCategory Category,

    GrandSize GrandSize,
    int CoffeeGrams,
    string? Author,
    int TotalWaterGrams,
    List<RecipeStepModel> Steps,
    List<RecipeReviewModel> Reviews
    );
