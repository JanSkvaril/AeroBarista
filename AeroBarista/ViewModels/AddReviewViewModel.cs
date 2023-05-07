using AeroBarista.ApiClients.Interfaces;
using AeroBarista.Attributes;
using AeroBarista.Shared.Models;
using AeroBarista.Services.Interfaces;
using AeroBarista.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AeroBarista.ViewModels
{
    [ExportTransient]
    [QueryProperty(nameof(RecipeId), nameof(RecipeId))]
    public partial class AddReviewViewModel : BaseViewModel
    {
        private readonly IRecipesReviewApiClient apiClient;
        private readonly IRecipeApiClient apiRecipeClient;

        [ObservableProperty]
        private int recipeId;
        [ObservableProperty]
        private int rating;
        [ObservableProperty]
        private string text;

        public AddReviewViewModel(INavigationService navigationService, IRecipesReviewApiClient apiClient, IRecipeApiClient apiRecipeClient) : base(navigationService)
        {
            Text = string.Empty;
            this.apiClient = apiClient;
            this.apiRecipeClient = apiRecipeClient;
        }

        partial void OnRecipeIdChanged(int value)
        {
            Reset();
        }

        [RelayCommand]
        public async void SaveReview()
        {
            // todo change here!
            Random rnd = new Random();
            int id = rnd.Next();

            await apiClient.Create(new RecipeReviewModel(id, RecipeId, Rating, Text, DateTime.Now));
            var x = await apiClient.GetRecipeReviews(RecipeId);

            var recip = await apiRecipeClient.GetAll();
            var y = recip.First(r => r.Id == RecipeId);

            Reset();

            var parameters = new Dictionary<string, object> { [nameof(DetailRecipeViewModel.Id)] = RecipeId };
            await NavigationService.NavigateToAsync("DetailRecipePage", parameters);
        }

        private void Reset()
        {
            Text = string.Empty;
            Rating = 0;
        }
    }
}
