using AeroBarista.Services.Interfaces;
using AeroBarista.ViewModels.Base;
using AeroBarista.Views;
using CommunityToolkit.Mvvm.Input;

namespace AeroBarista.ViewModels;

public partial class FirstLaunchPageViewModel : BaseViewModel
{
	public FirstLaunchPageViewModel(INavigationService navigationService) : base(navigationService)
	{

	}

	//[RelayCommand]
	//public void NavigateToHomePage()
	//{
	//	NavigationService.NavigateToAsync(nameof(HomePage));
	//}
}
