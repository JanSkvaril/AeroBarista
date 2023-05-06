using AeroBarista.Enums;
using AeroBarista.Models;

namespace AeroBarista.Database;

public class DemoDatabase
{
    List<RecipeModel> data;

    public DemoDatabase()
    {
        data = new();
        SeedData();
    }

    #region recipe

    public void CreateRecipe(RecipeModel recipe) => data.Add(recipe);
    public bool UpdateRecipe(RecipeModel recipe)
    {
        var recipeIndex = data.IndexOf(recipe);

        if (recipeIndex == -1)
            throw new ArgumentException("Recipe not found in the list.");

        return (data[recipeIndex] = recipe) != null;
    }
    //Real database won't delete, it will only set Removed flag
    public bool DeleteRecipe(int recipeId)
    {
        var recipe = data.FirstOrDefault(r => r.Id == recipeId);
        if (recipe == null)
            throw new ArgumentNullException(nameof(recipe));

        return data.Remove(recipe);
    }
    public List<RecipeModel> GetAllRecipes() => data;
    public RecipeModel? GetRecipe(int id) => data.FirstOrDefault(r => r.Id == id);

    #endregion

    #region steps

    public void CreateRecipeStep(int recipeId, RecipeStepModel recipeStep)
    {
        var recipe = data.FirstOrDefault(r => r.Id == recipeId);
        if (recipe == null)
            throw new ArgumentNullException(nameof(recipe));

        recipe.Steps.Add(recipeStep);
    }

    public bool UpdateRecipeStep(RecipeStepModel recipeStep)
    {
        var recipe = data.FirstOrDefault(r => r.Id == recipeStep.RecipeId);
        if (recipe == null)
            throw new ArgumentNullException(nameof(recipe));

        var stepIndex = recipe.Steps?.IndexOf(recipeStep) ?? -1;
        if (stepIndex == -1)
            throw new KeyNotFoundException($"Index {stepIndex} not found.");

        return (recipe.Steps![stepIndex] = recipeStep) != null;
    }

    public bool DeleteStep(int recipeStepId)
    {
        foreach (var recipe in data)
        {
            foreach (var step in recipe.Steps)
            {
                if (step.Id == recipeStepId)
                    return recipe.Steps.Remove(step);
            }
        }
        return false;
    }

    #endregion

    #region reviews

    public void CreateReview(int recipeId, RecipeReviewModel recipeReview)
    {
        var recipe = data.FirstOrDefault(r => r.Id == recipeId);
        if (recipe == null)
            throw new ArgumentNullException(nameof(recipe));

        recipe.Reviews.Add(recipeReview);
    }
    public bool UpdateReview(RecipeReviewModel review)
    {
        var recipe = data.FirstOrDefault(r => r.Id == review.RecipeId);
        if (recipe == null)
            throw new ArgumentNullException(nameof(recipe));

        var reviewIndex = recipe.Reviews?.IndexOf(review) ?? -1;
        if (reviewIndex == -1)
            throw new KeyNotFoundException($"Index {reviewIndex} not found.");

        return (recipe.Reviews![reviewIndex] = review) != null;
    }
    public bool DeleteReview(int reviewId)
    {
        foreach (var recipe in data)
        {
            foreach (var review in recipe.Reviews)
            {
                if(review.Id == reviewId)
                    return recipe.Reviews.Remove(review);
            }
        }
        return false;
    }
    public List<RecipeReviewModel> GetRecipeReviews(int recipeId)
    {
        var recipe = GetRecipe(recipeId);
        if (recipe == null)
            throw new ArgumentNullException(nameof(recipe));

        return recipe.Reviews;
    }

    #endregion

    #region seed

    private void SeedData()
    {
        data.Add(
            new RecipeModel(
                1,
                "MyCustomBestRecipe",
                "Recipe stolen from some random article. Is has large coffe-water ratio, so it's quite cheap. It's generally better for lighter roastes. For Darker roasts I recomment reducing the water tempeture to 90 degres",
                RecipeMethod.Standard,
                RecipeCategory.Default,
                GrandSize.Fine,
                12,
                "Franta Zahradnik",
                200,
                true,
                new()
                {
                      new(
                        0,
                        1,
                        0,
                        "Prepare yourself!",
                        "nic :)",
                        StepType.Simple,
                        null,
                        null
                    ),
                    new(
                        1,
                        1,
                        1,
                        "Boil the water to 98 degress",
                        "For lighter roasts I recomend 90 degress",
                        StepType.Simple,
                        null,
                        TimeSpan.FromSeconds(5)
                    ),
                    new(
                        2,
                        1,
                        2,
                        "Grind 12 grams (fine)",
                        "Very loo oo ooooo ooooo oooo ooo oo oo ooo ooo oo oo oo ong description",
                        StepType.Grounds,
                        null,
                        TimeSpan.FromSeconds(5)
                    ),
                    new(
                        3,
                        1,
                        3,
                        "Add the filter (no rinse) and the grounds",
                        string.Empty,
                        StepType.Movement,
                        null,
                        TimeSpan.FromSeconds(5)
                    ),
                    new(
                        4,
                        1,
                        4,
                        "Slowly poor 150 grams of water",
                        string.Empty,
                        StepType.Water,
                        150,
                        TimeSpan.FromSeconds(60)
                    ),
                    new(
                        5,
                        1,
                        5,
                        "Wait 10 seconds",
                        string.Empty,
                        StepType.Wait,
                        null,
                        TimeSpan.FromSeconds(10)
                    ),
                    new(
                        6,
                        1,
                        6,
                        "Poor the rest of the foter (50 grams)",
                        string.Empty,
                        StepType.Simple,
                        null,
                        TimeSpan.FromSeconds(15)
                    ),
                    new(
                        7,
                        1,
                        7,
                        "Boil the water to 98 degress",
                        string.Empty,
                        StepType.Water,
                        null,
                        TimeSpan.FromSeconds(180)
                    ),
                    new(
                        8,
                        1,
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
                        1,
                        1,
                        5,
                        "Enthiopia from Peneriny roastery in Brno, grinder was set to 10 steps. It was ok, but next time go finer with the grind",
                         new DateTime(2023,3,15)
                     )
                }
            )
        );

        data.Add(
            new RecipeModel(
                2,
                "ReceptFromBestDrive",
                null,
                RecipeMethod.Standard,
                RecipeCategory.UserDefined,
                GrandSize.Medium,
                14,
                "Pan Varil",
                100,
                false,
                new()
                {
                    new(
                        1,
                        1,
                        1,
                        "Boil the water to 98 degress",
                        "For lighter roasts I recomend 90 degress",
                        StepType.Simple,
                        null,
                        TimeSpan.FromSeconds(5)
                    ),
                    new(
                        2,
                        1,
                        2,
                        "Grind 12 grams (fine)",
                        string.Empty,
                        StepType.Simple,
                        null,
                        TimeSpan.FromSeconds(5)
                    ),
                    new(
                        3,
                        1,
                        3,
                        "Add the filter (no rinse) and the grounds",
                        string.Empty,
                        StepType.Simple,
                        null,
                        TimeSpan.FromSeconds(5)
                    ),
                    new(
                        4,
                        1,
                        4,
                        "Slowly poor 150 grams of water",
                        string.Empty,
                        StepType.Water,
                        150,
                        TimeSpan.FromSeconds(60)
                    ),
                    new(
                        5,
                        1,
                        5,
                        "Wait 10 seconds",
                        string.Empty,
                        StepType.Simple,
                        null,
                        TimeSpan.FromSeconds(10)
                    ),
                    new(
                        6,
                        1,
                        6,
                        "Poor the rest of the foter (50 grams)",
                        string.Empty,
                        StepType.Simple,
                        null,
                        TimeSpan.FromSeconds(15)
                    ),
                    new(
                        7,
                        1,
                        7,
                        "Boil the water to 98 degress",
                        string.Empty,
                        StepType.Water,
                        null,
                        TimeSpan.FromSeconds(180)
                    ),
                    new(
                        8,
                        1,
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
                        1,
                        1,
                        5,
                        "Enthiopia from Peneriny roastery in Brno, grinder was set to 10 steps. It was ok, but next time go finer with the grind",
                         new DateTime(2023,3,15)
                     )
                }
            )
        );

        data.Add(
            new RecipeModel(
                3,
                "ReceptFromPetrovice",
                "Recipe stolen from some random article. Is has large coffe-water ratio, so it's quite cheap. It's generally better for lighter roastes. For Darker roasts I recomment reducing the water tempeture to 90 degres",
                RecipeMethod.Standard,
                RecipeCategory.UserDefined,
                GrandSize.Medium,
                14,
                null,
                100,
                true,
                new()
                {
                    new(
                        1,
                        1,
                        1,
                        "Boil the water to 98 degress",
                        "For lighter roasts I recomend 90 degress",
                        StepType.Simple,
                        null,
                        TimeSpan.FromSeconds(5)
                    ),
                    new(
                        2,
                        1,
                        2,
                        "Grind 12 grams (fine)",
                        "Grind 12 grams fine",
                        StepType.Simple,
                        null,
                        TimeSpan.FromSeconds(5)
                    ),
                    new(
                        3,
                        1,
                        3,
                        "Add the filter (no rinse) and the grounds",
                        string.Empty,
                        StepType.Simple,
                        null,
                        TimeSpan.FromSeconds(5)
                    ),
                    new(
                        4,
                        1,
                        4,
                        "Slowly poor 150 grams of water",
                        string.Empty,
                        StepType.Water,
                        150,
                        TimeSpan.FromSeconds(60)
                    ),
                    new(
                        5,
                        1,
                        5,
                        "Wait 10 seconds",
                        string.Empty,
                        StepType.Simple,
                        null,
                        TimeSpan.FromSeconds(10)
                    ),
                    new(
                        6,
                        1,
                        6,
                        "Poor the rest of the foter (50 grams)",
                        string.Empty,
                        StepType.Simple,
                        null,
                        TimeSpan.FromSeconds(15)
                    ),
                    new(
                        7,
                        1,
                        7,
                        "Boil the water to 98 degress",
                        string.Empty,
                        StepType.Water,
                        null,
                        TimeSpan.FromSeconds(180)
                    ),
                    new(
                        8,
                        1,
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
                        1,
                        1,
                        3,
                        "Enthiopia from Peneriny roastery in Brno, grinder was set to 10 steps. It was ok, but next time go finer with the grind",
                         new DateTime(2023,3,15)
                     )
                }
            )
        );
    }


    #endregion
}
