using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;
using Microsoft.Maui.Controls;

namespace AeroBarista;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
[IntentFilter(new[] { Intent.ActionView },
    Categories = new[]
    {
        Intent.ActionView,
        Intent.CategoryDefault,
        Intent.CategoryBrowsable
    },
    DataScheme = "https", DataHost = "aeropress", DataPathPrefix = "/recipe"
    )
]
public class MainActivity : MauiAppCompatActivity
{
    int? idParameter = null;
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        //test
        OnNewIntent(Intent);
    }
    protected override void OnResume()
    {
        base.OnResume();

        var fromDeepLink = Preferences.Get("FromDeepLink", false);
        if (fromDeepLink)
        {
            if (idParameter == null) return;
            Preferences.Set("FromDeepLink", false);
            var parameters = new Dictionary<string, object> { { "Id", idParameter } };
            Shell.Current.GoToAsync("//RecipesPage/DetailRecipePage", parameters);

        }
    }


    protected override void OnNewIntent(Intent intent)
    {
        base.OnNewIntent(intent);

        var data = intent.DataString;

        if (intent.Action != Intent.ActionView) return;
        if (string.IsNullOrWhiteSpace(data)) return;

        if (data.Contains("/recipe"))
        {
            idParameter = int.Parse(data.Split("/").Last());
            Preferences.Set("FromDeepLink", true);

        }
    }
}
