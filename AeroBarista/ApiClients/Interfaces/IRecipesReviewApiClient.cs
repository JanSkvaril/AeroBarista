using AeroBarista.Models;

namespace AeroBarista.ApiClients.Interfaces;

interface IRecipesReviewApiClient
{
    public Task Create(RecipeReviewModel recipeReview);
    public Task<bool> Update(RecipeReviewModel recipeReview);
    public Task<bool> Delete(int recipeReviewId);
    public Task<ICollection<RecipeReviewModel>> GetRecipeReviews(int recipeId);

}
