using AeroBarista.Attributes;
using AeroBarista.Shared.Models;
using AeroBarista.ViewModels;
using AeroBarista.ViewModels.Base;
using CommunityToolkit.Maui.Views;

namespace AeroBarista.Views;

[ExportTransient]
public partial class CreateStepPopUp : Popup
{
	public CreateStepPopUp(CreateStepViewModel createStepViewModel)
	{
		InitializeComponent();
		BindingContext = createStepViewModel;
		createStepViewModel.SaveResult += Save;
        base.Opened += CreateStepPopUp_Opened;

    }

    private void CreateStepPopUp_Opened(object? sender, CommunityToolkit.Maui.Core.PopupOpenedEventArgs e)
    {
        if (BindingContext is CreateStepViewModel viewModel)
            viewModel.OnOpen();
    }

    private void Save(RecipeStepModel step) => Close(step);
}