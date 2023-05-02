using AeroBarista.Attributes;
using AeroBarista.Enums;
using AeroBarista.ViewModels;

namespace AeroBarista.Views;

[ExportTransient]
public partial class RecipesPage : ContentPage
{
	private readonly RecipesViewModel viewModel;
	
	public RecipesPage(RecipesViewModel vm)
	{
		InitializeComponent();
		viewModel = vm;
		BindingContext = vm;
	}

	public void OnTextChanged(object sender, EventArgs e)
	{
        viewModel.Search();
	}

	public void OnCategoryChanged(object sender, CheckedChangedEventArgs e)
	{
		RadioButton radio = (RadioButton)sender;
		if (radio == null)
		{
			return;
		}

		Label label = (Label)radio.Content;

		if (e.Value && label != null)
		{
			if (label.Text == "Default")
			{
				viewModel.Filter(RecipeCategory.Default);
			}
			else if (label.Text == "My")
			{
				viewModel.Filter(RecipeCategory.UserDefined);
			}
			else if (label.Text == "Imported")
			{
				viewModel.Filter(RecipeCategory.Imported);
			}
			else
			{
                viewModel.AllRecipe();
            }
		}
	}
}