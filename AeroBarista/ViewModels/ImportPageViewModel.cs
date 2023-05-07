using AeroBarista.Attributes;
using AeroBarista.Services.Interfaces;
using AeroBarista.ViewModels.Base;
using AndroidX.Lifecycle;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroBarista.ViewModels
{
    [ExportTransient]
    public partial class ImportPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        string statusMessage;

        [ObservableProperty]
        bool isBusy = false;

        ISettingsProvider settings;
        public ImportPageViewModel(INavigationService navigationService, ISettingsProvider settings) : base(navigationService)
        {
            this.settings = settings;
        }


        [RelayCommand]
        public async Task QRDetected(string value)
        {
            if (value.Contains(settings.GetAppUrl())){
                string? recipe_id = value.Split("/").LastOrDefault();
                if (recipe_id != null) {
                    IsBusy = true;
                    StatusMessage = "QR code detected";
                    int id = int.Parse(recipe_id);
                    var parameters = new Dictionary<string, object> { [nameof(DetailRecipeViewModel.Id)] = id };
                    await NavigationService.NavigateToAsync("//RecipesPage/DetailRecipePage", parameters);
                    IsBusy = false;
                    StatusMessage = "";
                }
            }
            else
            {
                StatusMessage = "QR code does not contain recipe";
            }
        }
    }
}
