using AeroBarista.Shared.Enums;

namespace AeroBarista.Shared.Models;

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
    bool IsFavourite,
    List<RecipeStepModel> Steps,
    List<RecipeReviewModel> Reviews
    );
