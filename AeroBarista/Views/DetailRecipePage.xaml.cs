using AeroBarista.Attributes;
using AeroBarista.Models;
using AeroBarista.ViewModels;
using AeroBarista.ViewModels.Base;
using CommunityToolkit.Maui.Views;

namespace AeroBarista.Views;

[ExportTransient]
public partial class DetailRecipePage : ContentPage
{
    private readonly DetailRecipeViewModel viewModel;

    public DetailRecipePage(DetailRecipeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        viewModel = vm;
        vm.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(viewModel.Recipe))
            {
                var steps = new List<StepIconModel>();
                if (viewModel.Recipe == null)
                {
                    return;
                }

                foreach (var step in viewModel.Recipe.Steps)
                {
                    var path = "";
                    if (step.StepType == Enums.StepType.Water)
                    {
                        path = "droplet.png";
                    }
                    else if (step.StepType == Enums.StepType.Grounds)
                    {
                        path = "coffeebean.png";
                    }
                    else if (step.StepType == Enums.StepType.Wait)
                    {
                        path = "clock.png";
                    }
                    else if (step.StepType == Enums.StepType.Movement)
                    {
                        path = "arrowsspin.png";
                    }

                    steps.Add(new StepIconModel(step.Id, step.RecipeId, step.Order, step.ShorText, step.Description,
                    step.StepType, step.Value, step.time, path));
                }
                collectionSteps.ItemsSource = steps;
            }
            return;
        };
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