using AeroBarista.ApiClients.Interfaces;
using AeroBarista.Attributes;
using AeroBarista.Services.Interfaces;
using AeroBarista.ViewModels.Base;

namespace AeroBarista.ViewModels;

[ExportTransient]
public class HomePageViewModel : BaseViewModel
{
    IRecipeApiClient apiClient;

    public HomePageViewModel(INavigationService navigationService, IRecipeApiClient apiClient) : base(navigationService)
    {
        this.apiClient = apiClient;
        GetData();
    }

    private async void GetData()
    {
        var x = await apiClient.GetAll();
    }
}
