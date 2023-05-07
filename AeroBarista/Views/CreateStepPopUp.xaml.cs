using AeroBarista.Attributes;
using AeroBarista.Shared.Models;
using AeroBarista.ViewModels;
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
	}

	private void Save(RecipeStepModel step) => Close(step);
}