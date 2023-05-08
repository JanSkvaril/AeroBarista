using AeroBarista.Attributes;
using AeroBarista.Shared.Models;

namespace AeroBarista.Services;

[ExportSingleton]
public class CurrentDataProvider
{
	private RecipeModel? currentRecipe;
	public RecipeModel? CurrentRecipe
	{
		get 
		{
			var recipe = currentRecipe;
			currentRecipe = null;
			return recipe;
		}
        set => currentRecipe = value;
	}

    private RecipeStepModel? currentRecipeStep;
    public RecipeStepModel? CurrentRecipeStep
    {
        get
        {
            var recipeStep = currentRecipeStep;
            currentRecipeStep = null;
            return recipeStep;
        }
        set => currentRecipeStep = value;
    }
}
