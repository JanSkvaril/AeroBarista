using AeroBarista.Attributes;
using AeroBarista.Shared.Models;
using AeroBarista.ViewModels;
using CommunityToolkit.Maui.Views;
using System.ComponentModel;

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

    private async void DisplayPopupAsync(Popup popup, int? editStepIndex)
	{
		var result = await this.ShowPopupAsync(popup) as RecipeStepModel;

        if (result == null)
            return;

        if (editStepIndex != null)
        {
            createRecipeViewModel.Steps[(int)editStepIndex] = result;
            createRecipeViewModel.OnStepsChanged();
            return;
        }

        createRecipeViewModel.Steps.Add(result);
        createRecipeViewModel.OnStepsChanged();
    }
}