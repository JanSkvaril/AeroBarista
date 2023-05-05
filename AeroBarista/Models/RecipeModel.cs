﻿using AeroBarista.Enums;

namespace AeroBarista.Models;

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
