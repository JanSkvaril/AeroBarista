using AeroBarista.Enums;

namespace AeroBarista.Models;

public record RecipeStepModel(
    int id,
    string ShorText,
    string? Description,
    StepType StepType,
    int? Value,
    TimeSpan time
    );
