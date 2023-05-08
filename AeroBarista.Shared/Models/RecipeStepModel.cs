using AeroBarista.Shared.Enums;

namespace AeroBarista.Shared.Models;

public record RecipeStepModel(
    int Id,
    int RecipeId,
    int Order,
    string ShorText,
    string? Description,
    StepType StepType,
    int? Value,
    TimeSpan? Time
    );
