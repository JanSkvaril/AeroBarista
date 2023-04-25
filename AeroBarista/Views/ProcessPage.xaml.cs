using AeroBarista.Attributes;
using AeroBarista.ViewModels;

namespace AeroBarista.Views;


[ExportTransient]
public partial class ProcessPage : ContentPage
{
	public ProcessPage(ProcessPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}