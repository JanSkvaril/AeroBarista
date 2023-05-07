using AeroBarista.Shared.Enums;
using AeroBarista.Shared.Models;

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

    public void CreateRecipe(RecipeModel recipe)
    {
        recipe = recipe with { Id = GetUniqueId() };
        data.Add(recipe);
    }
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
        recipeStep = recipeStep with { Id = GetUniqueId() };
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
        recipeReview = recipeReview with { Id = GetUniqueId() };
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

    private int GetUniqueId() => Guid.NewGuid().GetHashCode();

    #region seed

    private void SeedData()
    {
        data.Add(
            new RecipeModel(
                1,
                "James Hoffman - standard",
                "Recipe stolen from some random article. Is has large coffe-water ratio, so it's quite cheap. It's generally better for lighter roastes. For Darker roasts I recomment reducing the water tempeture to 90 degres",
                RecipeMethod.Standard,
                RecipeCategory.Default,
                GrandSize.Medium,
                11,
                "James Hoffman",
                200,
                true,
                new()
                {
                    new(
                        0,
                        1,
                        0,
                        "Add filter",
                        "No need to rinse.",
                        StepType.Simple,
                        null,
                        null),
                    new(
                        1,
                        1,
                        1,
                        "Add 11g grounds",
                        "For light roast, it should be ground fairly fine, but for medium or dark rost go coarser.",
                        StepType.Grounds,
                        11,
                        null
                    ),
                    new(
                        2,
                        1,
                        2,
                        "Pour 200g of water",
                        "Close to boiling for light roast, 85°C for dark roast.",
                        StepType.Water,
                        200,
                        TimeSpan.FromSeconds(30)
                    ),
                    new(
                        3,
                        1,
                        3,
                        "Insert the plunger and lift it up slightly.Then wait",
                        string.Empty,
                        StepType.Wait,
                        null,
                        TimeSpan.FromSeconds(90)
                    ),
                    new(
                        4,
                        1,
                        4,
                        "Give it a litte swirl",
                        string.Empty,
                        StepType.Movement,
                        null,
                        TimeSpan.FromSeconds(5)
                    ),
                    new(
                        5,
                        1,
                        5,
                        "Wait",
                        string.Empty,
                        StepType.Wait,
                        null,
                        TimeSpan.FromSeconds(25)
                    ),
                    new(
                        6,
                        1,
                        6,
                        "Press gently into the cup until the hiss",
                        string.Empty,
                        StepType.Simple,
                        null,
                        TimeSpan.FromSeconds(30)
                    ),                  
                },
                new()
                {
                    new(
                        1,
                        1,
                        5,
                        "Enthiopia from Peneriny roastery in Brno, grinder was set to 10 steps. It was ok, but next time go finer with the grind. 15 grams works better",
                         new DateTime(2023,3,15)
                     )
                }
            )
        );

        data.Add(
            new RecipeModel(
                2,
                "Blue Bottle",
                null,
                RecipeMethod.Inverted,
                RecipeCategory.UserDefined,
                GrandSize.Medium,
                15,
                "bluebottlecoffee.com",
                200,
                false,
                new()
                {
                    new(
                        1,
                        1,
                        1,
                        "Place a filter with rinse",
                        "Rinse with hot water preferably.",
                        StepType.Simple,
                        null,
                        null
                    ),
                    new(
                        2,
                        1,
                        2,
                        "Add 15g of ground.",
                        "It should be slightly finer then sea salt",
                        StepType.Grounds,
                        15,
                        null
                    ),
                    new(
                        3,
                        1,
                        3,
                        "Pour 30 grams of water",
                        string.Empty,
                        StepType.Water,
                        30,
                        TimeSpan.FromSeconds(10)
                    ),
                    new(
                        4,
                        1,
                        4,
                        "Tamp the ground with paddle or spoon. Then let it bloom",
                        string.Empty,
                        StepType.Wait,
                        20,
                        TimeSpan.FromSeconds(20)
                    ),
                    new(
                        5,
                        1,
                        5,
                        "Pour aditional 170g of water",
                        string.Empty,
                        StepType.Water,
                        null,
                        TimeSpan.FromSeconds(10)
                    ),
                    new(
                        6,
                        1,
                        6,
                        "Let it steep",
                        string.Empty,
                        StepType.Wait,
                        null,
                        TimeSpan.FromSeconds(50)
                    ),
                    new(
                        7,
                        1,
                        7,
                        "Stir 10 times",
                        string.Empty,
                        StepType.Movement,
                        null,
                        TimeSpan.FromSeconds(10)
                    ),
                    new(
                        8,
                        1,
                        8,
                        "Fasten the filter cap, flip, and press into the cup",
                        string.Empty,
                        StepType.Movement,
                        null,
                        TimeSpan.FromSeconds(40)
                    )
                },
                new()
                {
                    new(
                        1,
                        1,
                        3,
                        "Meh",
                         new DateTime(2023,3,15)
                     )
                }
            )
        );

        data.Add(
            new RecipeModel(
                3,
                "WAC 2015 Winner",
                "Winning recipe from WAC 2015 winner",
                RecipeMethod.Inverted,
                RecipeCategory.UserDefined,
                GrandSize.Fine,
                20,
                "Lukas Zahradnik",
                230,
                true,
                new()
                {
                    new(
                        1,
                        1,
                        1,
                        "Rinse the paper",
                        "",
                        StepType.Simple,
                        null,
                        null
                    ),
                    new(
                        2,
                        1,
                        2,
                        "Preheat your aeropress to 79°C",
                        "It should not take more then 15 seconds",
                        StepType.Simple,
                        null,
                        null
                    ),
                    new(
                        3,
                        1,
                        3,
                        "Add 20g grounds",
                        string.Empty,
                        StepType.Grounds,
                        20,
                        null
                    ),
                    new(
                        4,
                        1,
                        4,
                        "Pour 60g of hot water",
                        string.Empty,
                        StepType.Water,
                        60,
                        TimeSpan.FromSeconds(15)
                    ),
                    new(
                        5,
                        1,
                        5,
                        "Give AeroPress a turbulet wiggle",
                        string.Empty,
                        StepType.Movement,
                        null,
                        TimeSpan.FromSeconds(15)
                    ),
                    new(
                        6,
                        1,
                        6,
                        "Pour additional 170g of hot water",
                        string.Empty,
                        StepType.Water,
                        null,
                        TimeSpan.FromSeconds(10)
                    ),
                    new(
                        7,
                        1,
                        7,
                        "Fasten the filter cap, flip, and slowly press into a cup",
                        string.Empty,
                        StepType.Movement,
                        null,
                        TimeSpan.FromSeconds(45)
                    ),

                },
                new()
                {

                }
            )
        );
    }


    #endregion
}
