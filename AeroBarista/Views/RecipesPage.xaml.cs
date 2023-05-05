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
		viewModel = vm;
		BindingContext = vm;
	}

    protected override async void OnAppearing()
	{
        base.OnAppearing();
		await viewModel.OnAppearingAsync();
    }


    public void OnTextChanged(object sender, EventArgs e)
	{
		viewModel.Search();
	}
}