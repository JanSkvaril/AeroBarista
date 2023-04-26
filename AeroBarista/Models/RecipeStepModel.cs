using AeroBarista.Enums;

namespace AeroBarista.Models;

public record RecipeStepModel(
    int Id,
    int RecipeId,
    int Order,
    string ShorText,
    string? Description,
    StepType StepType,
    int? Value,
    TimeSpan? time
    );
