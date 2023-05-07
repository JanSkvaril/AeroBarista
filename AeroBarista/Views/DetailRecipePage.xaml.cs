using AeroBarista.Attributes;
using AeroBarista.Models;
using AeroBarista.ViewModels;
using AeroBarista.ViewModels.Base;
using CommunityToolkit.Maui.Views;

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

    public async void OptionsButtonClicked(object sender, EventArgs args)
    {
        var popup = new RecipeOptionsPage();
        await this.ShowPopupAsync(popup);
    }
}