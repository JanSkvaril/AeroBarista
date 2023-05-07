using AeroBarista.ApiClients.Interfaces;
using AeroBarista.Attributes;
using AeroBarista.Models;
using AeroBarista.Services;
using AeroBarista.Services.Interfaces;
using AeroBarista.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AeroBarista.ViewModels
{
    [ExportTransient]
    [QueryProperty(nameof(Recipe), nameof(Recipe))]
    public partial class QRSharePageViewModel : BaseViewModel
    {
        [ObservableProperty]
        byte[] qrCodeBytes;

        [ObservableProperty]
        private RecipeModel? recipe;

        private readonly ISettingsProvider settingsProvider;
        private readonly INativeShareService share;

        public QRSharePageViewModel(INavigationService navigationService, ISettingsProvider settingsProvider, INativeShareService share) : base(navigationService)
        {
            this.settingsProvider = settingsProvider;
            this.share = share;
        }

        [RelayCommand]
        public void ShareRecipe()
        {
            share.ShareURL(settingsProvider.GetAppUrl() + "/"+ Recipe.Id);
        }

        partial void OnRecipeChanged(RecipeModel? value)
        {
            QRRecipeService qr_service = new QRRecipeService();
            QrCodeBytes = qr_service.CreateQRCodeForRecipe(Recipe, settingsProvider.GetAppUrl());
        }
    }
}
