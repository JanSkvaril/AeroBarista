using AeroBarista.Enums;

namespace AeroBarista.Models;

public record RecipeModel(
    string Name,
    string? Description,
    RecipeMethod Method,
    GrandSize GrandSize,
    int CoffeeGrams,
    string? Author,
    int TotalWaterGrams,
    List<RecipeStepModel> Steps,
    List<ReviewModel> reviews
    );
