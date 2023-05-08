using AeroBarista.ApiClients.Interfaces;
using AeroBarista.Attributes;
using AeroBarista.Shared.Models;
using AeroBarista.Services.Interfaces;
using AeroBarista.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AeroBarista.ViewModels;

[ExportTransient]
public partial class HomePageViewModel : BaseViewModel
{
    private readonly IRecipeApiClient apiClient;
    private readonly ISloganService sloganService;

    private readonly Random random = new Random();

    [ObservableProperty]
    private string slogan;

    [ObservableProperty]
    private RecipeModel ratedRecipe;

    [ObservableProperty]
    private RecipeModel favouriteRecipe;

    [ObservableProperty]
    private RecipeModel randomRecipe;

    public HomePageViewModel(INavigationService navigationService, IRecipeApiClient apiClient, ISloganService sloganService) : base(navigationService)
    {
        this.apiClient = apiClient;
        GetData();
        this.sloganService = sloganService;
    }

    [RelayCommand]
    public async void RecipeDetail()
    {
        var parameters = new Dictionary<string, object> { [nameof(DetailRecipeViewModel.Id)] = RatedRecipe.Id };
        await NavigationService.NavigateToAsync("//RecipesPage/DetailRecipePage", parameters);
    }

    private async void GetData()
    {
        var recipes = await apiClient.GetAll();

        FindBestRatedRecipe(recipes);
        FindOneFavouriteRecipe(recipes);
        FindRandomRecipe(recipes);
        Slogan = await sloganService.GetRandomSlogan();
    }

    private void FindBestRatedRecipe(ICollection<RecipeModel> recipes)
    {
        if (recipes.Count == 0)
        {
            return;
        }

        var topRecipe = recipes.First();
        var topRating = CalculateRating(topRecipe);
        foreach (var actualRecipe in recipes)
        {
            var actualrating = CalculateRating(actualRecipe);
            if (actualrating > topRating)
            {
                topRecipe = actualRecipe;
                topRating = actualrating;
            }
        }

        RatedRecipe = topRecipe;
    }

    private int CalculateRating(RecipeModel recipe)
    {
        var rating = 0;

        foreach (var review in recipe.Reviews)
        {
            rating += review.Rating;
        }

        return rating;
    }

    private void FindOneFavouriteRecipe(ICollection<RecipeModel> recipes)
    {
        var favourite = recipes.Where(r => r.IsFavourite).ToList();
        int index = random.Next(0, favourite.Count);
        FavouriteRecipe = favourite[index];
    }

    private void FindRandomRecipe(ICollection<RecipeModel> recipes)
    {
        var randomRecipes = recipes.ToList();
        int index = random.Next(0, randomRecipes.Count);
        RandomRecipe = randomRecipes[index];
    }
}
