using AeroBarista.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AeroBarista.ViewModels.Base;

public class BaseViewModel
{
    protected INavigationService NavigationService { get; set; }

    public BaseViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }
}
