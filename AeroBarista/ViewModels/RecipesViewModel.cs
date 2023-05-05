using AeroBarista.ApiClients.Interfaces;
using AeroBarista.Attributes;
using AeroBarista.Enums;
using AeroBarista.Models;
using AeroBarista.Services.Interfaces;
using AeroBarista.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AeroBarista.ViewModels
{
    [ExportTransient]
    public partial class RecipesViewModel : BaseViewModel
    {
        IRecipeApiClient apiClient;

        [ObservableProperty]
        private IList<RecipeModel> recipes;
        private IList<RecipeModel> allRecipes;

        [ObservableProperty]
        private string filterName;
        private RecipeCategory filterCategory;

        public RecipesViewModel(INavigationService navigationService, IRecipeApiClient apiClient) : base(navigationService)
        {
            this.apiClient = apiClient;
            Recipes = new List<RecipeModel>();
            allRecipes = new List<RecipeModel>();
            GetDataAsync();
            filterName = String.Empty;
            filterCategory = RecipeCategory.All;
        }

        public override async Task OnAppearingAsync()
        {
            await base.OnAppearingAsync();
            GetDataAsync();
        }

        private async void GetDataAsync()
        {
            var actual = await apiClient.GetAll();
            Recipes = actual.ToList();
            allRecipes = actual.ToList();
        }

        [RelayCommand]
        public async void RecipeDetail(int id)
        {
            var parameters = new Dictionary<string, object> { [nameof(DetailRecipeViewModel.Id)] = id };
            await NavigationService.NavigateToAsync("//DetailRecipePage", parameters);
        }

        [RelayCommand]
        public void Search()
        {
            var actualRecipes = Recipes;
            if (filterCategory == RecipeCategory.All)
            {
                actualRecipes = allRecipes.ToList();
            }
            else if (filterCategory == RecipeCategory.Favourite)
            {
                actualRecipes = allRecipes.Where(r => r.IsFavourite).ToList();
            }
            else
            {
                actualRecipes = allRecipes.Where(r => r.Category == filterCategory).ToList();
            }

            Recipes = actualRecipes.Where(r => r.Name.ToLower().Contains(FilterName.ToLower())).ToList();
        }

        public void Filter(RecipeCategory category)
        {
            filterCategory = category;
            var actualRecipes = allRecipes.Where(r => r.Category == category).ToList();
            Recipes = actualRecipes.Where(r => r.Name.ToLower().Contains(FilterName.ToLower())).ToList();
        }

        [RelayCommand]
        public void FilterWithName(string name)
        {
            if (name == "My")
            {
                Filter(RecipeCategory.UserDefined);
            }
            else if (name == "Default")
            {
                Filter(RecipeCategory.Default);
            }
            else if (name == "Imported")
            {
                Filter(RecipeCategory.Imported);
            }
            else if (name == "Favourite")
            {
                FavouriteRecipes();
            }
            else if (name == "All")
            {
                AllRecipes();
            }
        }

        public void AllRecipes()
        {
            filterCategory = RecipeCategory.All;
            Recipes = allRecipes.Where(r => r.Name.ToLower().Contains(FilterName.ToLower())).ToList();
        }

        public void FavouriteRecipes()
        {
            filterCategory = RecipeCategory.Favourite;
            Recipes = allRecipes.Where(r => r.IsFavourite).ToList();
        }
    }
}
