using AeroBarista.Attributes;
using AeroBarista.Shared.Models;
using AeroBarista.ViewModels;
using CommunityToolkit.Maui.Views;

namespace AeroBarista.Views;

[ExportTransient]
public partial class CreateRecipePage : ContentPage
{
    private readonly CreateRecipeViewModel createRecipeViewModel;

    public CreateRecipePage(CreateRecipeViewModel createRecipeViewModel)
	{
		InitializeComponent();
        this.createRecipeViewModel = createRecipeViewModel;
        BindingContext = createRecipeViewModel;
        createRecipeViewModel.DisplayPopup += DisplayPopupAsync;
    }

    private async void DisplayPopupAsync(Popup popup)
	{
		var result = await this.ShowPopupAsync(popup);
        createRecipeViewModel.Steps.Add(result as RecipeStepModel);
	}
}