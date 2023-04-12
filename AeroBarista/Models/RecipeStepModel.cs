using AeroBarista.Enums;

namespace AeroBarista.Models;

public record RecipeStepModel(
    string ShorText,
    string? Description,
    StepType StepType,
    int? Value
    );
