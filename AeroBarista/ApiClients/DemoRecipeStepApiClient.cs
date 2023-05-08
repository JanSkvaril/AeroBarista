using AeroBarista.ApiClients.Interfaces;
using AeroBarista.Database;
using AeroBarista.Shared.Models;

namespace AeroBarista.ApiClients;

class DemoRecipeStepApiClient : IRecipeStepApiClient
{
    private readonly DemoDatabase demoDatabase;

    public DemoRecipeStepApiClient(DemoDatabase demoDatabase) => this.demoDatabase = demoDatabase;

    public async Task<RecipeStepModel> Create(RecipeStepModel recipeStep) => await Task.Run(() => demoDatabase.CreateRecipeStep(recipeStep.RecipeId, recipeStep));

    public async Task<bool> Delete(int recipeStepId) => await Task.Run(() => demoDatabase.DeleteStep(recipeStepId));

    public async Task<bool> Update(RecipeStepModel recipeStep) => await Task.Run(() => demoDatabase.UpdateRecipeStep(recipeStep));
}
