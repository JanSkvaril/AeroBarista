namespace AeroBarista.Shared.Models;

public record RecipeReviewModel(int Id, int RecipeId, int Rating, string Text, DateTime date);
