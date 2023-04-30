using AeroBarista.ApiClients.Interfaces;
using AeroBarista.Attributes;
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
        private IList<RecipeModel>? recipes;

        public RecipesViewModel(INavigationService navigationService, IRecipeApiClient apiClient) : base(navigationService)
        {
            this.apiClient = apiClient;
            GetData();
        }

        private async void GetData()
        {
            var actual = await apiClient.GetAll();

            Recipes = actual.ToList();
        }

        [RelayCommand]
        public async void RecipeDetail(int id)
        {
            var parameters = new Dictionary<string, object> { [nameof(DetailRecipeViewModel.Id)] = id };
            await NavigationService.NavigateToAsync("//DetailRecipePage", parameters);
        }

        [RelayCommand]
        public async void Search(string findingName)
        {
            findingName = findingName.ToLower();

            var allRecipes = await apiClient.GetAll();

            Recipes = allRecipes.Where(r => r.Name.ToLower().Contains(findingName)).ToList();
        }
    }
}
