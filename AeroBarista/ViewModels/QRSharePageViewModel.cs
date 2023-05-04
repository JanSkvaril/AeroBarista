using AeroBarista.ApiClients.Interfaces;
using AeroBarista.Attributes;
using AeroBarista.Services;
using AeroBarista.Services.Interfaces;
using AeroBarista.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using QRCoder;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AeroBarista.ViewModels
{
    [ExportTransient]
    public partial class QRSharePageViewModel : BaseViewModel
    {
        [ObservableProperty]
        byte[] qrCodeBytes;

        IRecipeApiClient recipeClient;
        public QRSharePageViewModel(INavigationService navigationService, IRecipeApiClient recipeClient) : base(navigationService)
        {
     
            this.recipeClient = recipeClient;
            GetData();
        }


    


        private async void GetData()
        {
            var x = await recipeClient.GetAll();
       
            var Recipe = x.First();
            QRRecipeService qr_service = new QRRecipeService();
            // TODO: dont hardcode url, fix!
            QrCodeBytes = qr_service.CreateQRCodeForRecipe(Recipe, "https://aerobarista/recipe");
        }
    }
}
