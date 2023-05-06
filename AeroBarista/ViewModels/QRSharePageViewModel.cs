using AeroBarista.ApiClients.Interfaces;
using AeroBarista.Attributes;
using AeroBarista.Models;
using AeroBarista.Services;
using AeroBarista.Services.Interfaces;
using AeroBarista.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;

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

        public QRSharePageViewModel(INavigationService navigationService, ISettingsProvider settingsProvider) : base(navigationService)
        {
            this.settingsProvider = settingsProvider;
        }

        partial void OnRecipeChanged(RecipeModel? value)
        {
            QRRecipeService qr_service = new QRRecipeService();
            QrCodeBytes = qr_service.CreateQRCodeForRecipe(Recipe, settingsProvider.GetAppUrl());
        }
    }
}
