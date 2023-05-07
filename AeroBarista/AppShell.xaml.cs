using AeroBarista.Views;

namespace AeroBarista;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(x), typeof(x));
        Routing.RegisterRoute(nameof(CreateRecipePage), typeof(CreateRecipePage));
    }
}
