using AeroBarista.ApiClients.Interfaces;
using AeroBarista.Models;
using static Android.Content.ClipData;

namespace AeroBarista.ApiClients;

internal class DemoRecipeApiClient : IRecipeApiClient
{
    List<RecipeModel> recipes;

    public DemoRecipeApiClient()
    {
        SeedData();
    }

    public async Task Create(RecipeModel item) => await Task.Run(() => recipes.Add(item));

    public async Task<bool> Delete(RecipeModel item) => await Task.FromResult(recipes.Remove(item));

    public async Task<ICollection<RecipeModel>> GetAll() => await Task.FromResult<ICollection<RecipeModel>>(recipes);

    public async Task<bool> Update(RecipeModel item)
    {
        var recipeIndex = recipes.IndexOf(item);

        if (recipeIndex == -1) 
            throw new ArgumentException("Recipe not found in the list.");

        recipes[recipeIndex] = item;
        return await Task.FromResult(true);
    }

    private void SeedData()
    {

    }
}
