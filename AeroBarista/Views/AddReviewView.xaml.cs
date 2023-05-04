using AeroBarista.Attributes;
using CommunityToolkit.Maui.Views;

namespace AeroBarista.Views;

[ExportTransient]
public partial class AddReviewView : Popup
{
	public AddReviewView()
	{
		InitializeComponent();
	}
}