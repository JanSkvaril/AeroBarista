using AeroBarista.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AeroBarista.ViewModels.Base;

public class BaseViewModel : ObservableValidator
{
    protected INavigationService NavigationService { get; set; }

    public BaseViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }
    public virtual Task OnAppearingAsync()
    {
        return Task.CompletedTask;
    }
}
