using AeroBarista.Attributes;
using AeroBarista.Services.Interfaces;
using AeroBarista.ViewModels.Base;

namespace AeroBarista.ViewModels;

[ExportTransient]
public class HomePageViewModel : BaseViewModel
{
	public HomePageViewModel(INavigationService navigationService) : base(navigationService)
	{

	}
}
