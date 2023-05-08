using AeroBarista.Attributes;
using AeroBarista.ViewModels;

namespace AeroBarista.Views;

[ExportTransient]
public partial class FinishedPage : ContentPage
{
	public FinishedPage(FinishedPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}