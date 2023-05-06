using CommunityToolkit.Maui.Views;

namespace AeroBarista.Views;

public partial class RecipeOptionsPage : Popup
{
	public RecipeOptionsPage()
	{
		InitializeComponent();
	}

	public void CloseButtonClicked(object? sender, EventArgs e) => Close(true);
}