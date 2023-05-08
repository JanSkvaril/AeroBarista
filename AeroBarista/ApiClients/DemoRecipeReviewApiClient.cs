using AeroBarista.ApiClients.Interfaces;
using AeroBarista.Database;
using AeroBarista.Shared.Models;

namespace AeroBarista.ApiClients;

class DemoRecipeReviewApiClient : IRecipesReviewApiClient
{

    private readonly DemoDatabase demoDatabase;

    public DemoRecipeReviewApiClient(DemoDatabase demoDatabase) => this.demoDatabase = demoDatabase;

    public async Task Create(RecipeReviewModel recipeReview) => await Task.Run(() => demoDatabase.CreateReview(recipeReview.RecipeId, recipeReview));

    public async Task<bool> Delete(int recipeReviewId) => await Task.Run(() => demoDatabase.DeleteReview(recipeReviewId));

    public async Task<ICollection<RecipeReviewModel>> GetRecipeReviews(int recipeId) => await Task.Run(() => demoDatabase.GetRecipeReviews(recipeId));

    public async Task<bool> Update(RecipeReviewModel recipeReview) => await Task.Run(() => demoDatabase.UpdateReview(recipeReview));
}
