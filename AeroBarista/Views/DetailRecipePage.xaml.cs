using AeroBarista.Attributes;
using AeroBarista.ViewModels;

namespace AeroBarista.Views;

[ExportTransient]
public partial class DetailRecipePage : ContentPage
{
	public DetailRecipePage(DetailRecipeViewModel vm)
	{
		InitializeComponent();
		BindingContext= vm;
	}
}