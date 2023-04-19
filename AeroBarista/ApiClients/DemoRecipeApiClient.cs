using AeroBarista.ApiClients.Interfaces;
using AeroBarista.Attributes;
using AeroBarista.Enums;
using AeroBarista.Models;
using AeroBarista.Services.Interfaces;

namespace AeroBarista.ApiClients;

public class DemoRecipeApiClient : IRecipeApiClient
{
    List<RecipeModel> recipes;

    public DemoRecipeApiClient()
    {
        recipes = new List<RecipeModel>();
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
        recipes.Add(
            new RecipeModel("MyCustomRecipe",
                "Recipe stolen from some random article. Is has large coffe-water ratio, so it's quite cheap. It's generally better for lighter roastes. For Darker roasts I recomment reducing the water tempeture to 90 degres",
                RecipeMethod.Standard,
                GrandSize.Fine,
                12,
                "Franta Zahradnik",
                200,
                new()
                {
                    new(
                        1,
                        "Boil the water to 98 degress",
                        "For lighter roasts I recomend 90 degress",
                        StepType.Simple,
                        null,
                        TimeSpan.FromSeconds(5)
                    ),
                    new(
                        2,
                        "Grind 12 grams (fine)",
                        string.Empty,
                        StepType.Simple,
                        null,
                        TimeSpan.FromSeconds(5)
                    ),
                    new(
                        3,
                        "Add the filter (no rinse) and the grounds",
                        string.Empty,
                        StepType.Simple,
                        null,
                        TimeSpan.FromSeconds(5)
                    ),
                    new(
                        4,
                        "Slowly poor 150 grams of water",
                        string.Empty,
                        StepType.Water,
                        150,
                        TimeSpan.FromSeconds(60)
                    ),
                    new(
                        5,
                        "Wait 10 seconds",
                        string.Empty,
                        StepType.Simple,
                        null,
                        TimeSpan.FromSeconds(10)
                    ),
                    new(
                        6,
                        "Poor the rest of the foter (50 grams)",
                        string.Empty,
                        StepType.Simple,
                        null,
                        TimeSpan.FromSeconds(15)
                    ),
                    new(
                        7,
                        "Boil the water to 98 degress",
                        string.Empty,
                        StepType.Water,
                        null,
                        TimeSpan.FromSeconds(180)
                    ),
                    new(
                        8,
                        "Boil the water to 98 degress",
                        string.Empty,
                        StepType.Simple,
                        null,
                        TimeSpan.FromSeconds(30)
                    )
                },
                new()
                {
                    new(
                        5,
                        "Enthiopia from Peneriny roastery in Brno, grinder was set to 10 steps. It was ok, but next time go finer with the grind",
                         new DateTime(2023,3,15)
                     )
                }
            )
        );
    }
}
