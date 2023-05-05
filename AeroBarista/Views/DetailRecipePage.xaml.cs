using AeroBarista.Attributes;
using AeroBarista.ViewModels;
using AeroBarista.ViewModels.Base;

namespace AeroBarista.Views;

[ExportTransient]
public partial class DetailRecipePage : ContentPage
{
    public DetailRecipePage(DetailRecipeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is BaseViewModel viewModel)
        {
            await viewModel.OnAppearingAsync();
        }
    }
}