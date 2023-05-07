﻿using AeroBarista.ApiClients.Interfaces;
using AeroBarista.Attributes;
using AeroBarista.Shared.Models;
using AeroBarista.Services.Interfaces;
using AeroBarista.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AeroBarista.ViewModels
{
    [ExportTransient]
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class DetailRecipeViewModel : BaseViewModel
    {
        private readonly IRecipeApiClient apiClient;

        [ObservableProperty]
        private RecipeModel? recipe;

        [ObservableProperty]
        private TimeSpan totalTime;

        [ObservableProperty]
        private int id;

        public DetailRecipeViewModel(INavigationService navigationService, IRecipeApiClient apiClient) : base(navigationService)
        {
            this.apiClient = apiClient;
        }

        public override async Task OnAppearingAsync()
        {
            await base.OnAppearingAsync();
            if (Id != 0)
            {
                // Id is initialized for parameter
                GetDataAsync(Id);
            }
        }

        partial void OnIdChanged(int value)
        {
            GetDataAsync(value);
        }

        [RelayCommand]
        public async void StartRecipe()
        {
            var parameters = new Dictionary<string, object> { [nameof(ProcessPageViewModel.Recipe)] = Recipe };
            await NavigationService.NavigateToAsync("ProcessPage", parameters);
        }

        [RelayCommand]
        public async void DeleteRecipe()
        {
            await apiClient.Delete(Id);
            await NavigationService.NavigateToAsync("RecipesPage");
        }

        [RelayCommand]
        public async void AddReviewNavigate()
        {
            var parameters = new Dictionary<string, object> { [nameof(AddReviewViewModel.RecipeId)] = Recipe.Id };
            await NavigationService.NavigateToAsync("AddReview", parameters);
        }

        [RelayCommand]
        public async void ShareRecipe()
        {
            var parameters = new Dictionary<string, object> { [nameof(QRSharePageViewModel.Recipe)] = Recipe };
            await NavigationService.NavigateToAsync("QRSharePage", parameters);
        }

        private async void GetDataAsync(int id)
        {
            var actual = await apiClient.GetAll();
            Recipe = null;
            Recipe = actual.First(r => r.Id == id);

            GetTotalTime();
        }

        private void GetTotalTime()
        {
            TotalTime = new TimeSpan();

            if (Recipe == null)
            {
                return;
            }
            
            foreach (var step in Recipe.Steps)
            {
                if (step.time != null)
                {
                    TotalTime += (TimeSpan)step.time;
                }
            }
        }
    }
}
