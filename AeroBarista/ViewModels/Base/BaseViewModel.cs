using AeroBarista.Services.Interfaces;

namespace AeroBarista.ViewModels.Base;

public class BaseViewModel
{
    protected INavigationService NavigationService { get; set; }

    public BaseViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }
}
