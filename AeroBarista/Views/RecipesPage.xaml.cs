using AeroBarista.Attributes;
using AeroBarista.ViewModels;

namespace AeroBarista.Views;

[ExportTransient]
public partial class RecipesPage : ContentPage
{
	private readonly RecipesViewModel viewModel;
	
	public RecipesPage(RecipesViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		viewModel = vm;
	}

	public void OnTextChanged(object sender, EventArgs e)
	{
        viewModel.Search(searchBar.Text);
	}
}