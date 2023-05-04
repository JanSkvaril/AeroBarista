using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;
namespace AeroBarista;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]

[IntentFilter(new[] { Intent.ActionView },
    Categories = new[]
    {
        Intent.ActionView,
        Intent.CategoryDefault,
        Intent.CategoryBrowsable
    },
    DataScheme = "https", DataHost = "aerobarista", DataPathPrefix = "/recipe"
    )
]
public class MainActivity : MauiAppCompatActivity
{

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        //test
        OnNewIntent(Intent);
    }

    protected override void OnNewIntent(Intent intent)
    {
        base.OnNewIntent(intent);

        var data = intent.DataString;

        if (intent.Action != Intent.ActionView) return;
        if (string.IsNullOrWhiteSpace(data)) return;

        if (data.Contains("/group"))
        {
            //do what you want 
        }
    }
}
