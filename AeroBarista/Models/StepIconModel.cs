using AeroBarista.Enums;

namespace AeroBarista.Models
{
    public record StepIconModel(
        int Id,
        int RecipeId,
        int Order,
        string ShorText,
        string? Description,
        StepType StepType,
        int? Value,
        TimeSpan? Time,
        string IconPath
    );
}
