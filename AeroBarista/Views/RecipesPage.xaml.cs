using AeroBarista.Attributes;
using AeroBarista.ViewModels;

namespace AeroBarista.Views;

[ExportTransient]
public partial class RecipesPage : ContentPage
{
	public RecipesPage(RecipesViewModel vm)
	{
		InitializeComponent();
		BindingContext= vm;
	}
}