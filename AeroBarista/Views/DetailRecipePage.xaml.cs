using AeroBarista.Attributes;
using AeroBarista.ViewModels;
using CommunityToolkit.Maui.Views;

namespace AeroBarista.Views;

[ExportTransient]
public partial class DetailRecipePage : ContentPage
{
	public DetailRecipePage(DetailRecipeViewModel vm)
	{
		InitializeComponent();
		BindingContext= vm;
	}

    public void DisplayPopup(object sender, EventArgs args)
    {
        var popup = new AddReviewView();

        this.ShowPopup(popup);
    }
}