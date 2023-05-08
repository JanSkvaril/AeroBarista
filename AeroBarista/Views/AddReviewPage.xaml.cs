using AeroBarista.Attributes;
using AeroBarista.ViewModels;

namespace AeroBarista.Views;

[ExportTransient]
public partial class AddReviewPage : ContentPage
{
	public AddReviewPage(AddReviewViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}