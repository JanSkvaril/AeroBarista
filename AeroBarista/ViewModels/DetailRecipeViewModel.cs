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
    public partial class DetailRecipeViewModel : BaseViewModel
    {
        // TODO: get data from parameter, not api client!
        IRecipeApiClient apiClient;

        [ObservableProperty]
        private RecipeModel? recipe;

        [ObservableProperty]
        private TimeSpan totalTime;

        public DetailRecipeViewModel(INavigationService navigationService, IRecipeApiClient apiClient) : base(navigationService)
        {
            this.apiClient = apiClient;

            // change here!
            int id = 1;
            // change end

            GetData(id);
        }

        [RelayCommand]
        public async void StartRecipe(int id)
        {
            await NavigationService.NavigateToAsync("//ProcessPage");
        }

        private async void GetData(int id)
        {
            var actual = await apiClient.GetAll();

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
