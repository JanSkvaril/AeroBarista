using AeroBarista.Shared.Models;

namespace AeroBarista.ApiClients.Interfaces;

public interface IRecipeStepApiClient
{
    public Task Create(RecipeStepModel recipeStep);
    public Task<bool> Update(RecipeStepModel recipeStep);
    public Task<bool> Delete(int recipeStepId);
}
