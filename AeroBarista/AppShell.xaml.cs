using AeroBarista.Views;

namespace AeroBarista;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(x), typeof(x));
        Routing.RegisterRoute(nameof(ProcessPage), typeof(ProcessPage));
        Routing.RegisterRoute(nameof(FinishedPage), typeof(FinishedPage));
        Routing.RegisterRoute(nameof(QRSharePage), typeof(QRSharePage));
        Routing.RegisterRoute(nameof(DetailRecipePage), typeof(DetailRecipePage));
        Routing.RegisterRoute(nameof(RecipesPage), typeof(RecipesPage));
        Routing.RegisterRoute(nameof(AddReviewPage), typeof(AddReviewPage));
        Routing.RegisterRoute(nameof(ImportPage), typeof(ImportPage));
    }
}
