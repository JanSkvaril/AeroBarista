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

    private async void DisplayPopupAsync(Popup popup)
	{
		var result = await this.ShowPopupAsync(popup) as RecipeStepModel;

        if (result == null)
            return;

        var step = createRecipeViewModel.Steps.FirstOrDefault(s => s.Id == result.Id);

        if (step != null)
        {
            var stepIndex = createRecipeViewModel.Steps.IndexOf(step);
            createRecipeViewModel.Steps[stepIndex] = result;
            createRecipeViewModel.OnStepsChanged();
            return;
        }

        createRecipeViewModel.Steps.Add(result);
        createRecipeViewModel.OnStepsChanged();
    }
}