using AeroBarista.ApiClients.Interfaces;
using AeroBarista.Database;
using AeroBarista.Models;

namespace AeroBarista.ApiClients;

public class DemoRecipeApiClient : IRecipeApiClient
{
    private readonly DemoDatabase demoDatabase;

    public DemoRecipeApiClient(DemoDatabase demoDatabase) => this.demoDatabase = demoDatabase;

    public async Task Create(RecipeModel recipe) => await Task.Run(() => demoDatabase.CreateRecipe(recipe));

    public async Task<bool> Delete(int recipeId) => await Task.Run(() => demoDatabase.DeleteRecipe(recipeId));

    public async Task<ICollection<RecipeModel>> GetAll() => await Task.Run(() => demoDatabase.GetAllRecipes());

    public async Task<bool> Update(RecipeModel recipe) => await Task.Run(() => demoDatabase.UpdateRecipe(recipe));

}
