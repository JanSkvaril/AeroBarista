using AeroBarista.Attributes;
using AeroBarista.Services.Interfaces;

namespace AeroBarista.Services;

[ExportSingletonAs(nameof(INavigationService))]
public class NavigationService : INavigationService
{
    public Task NavigateToAsync(string route, IDictionary<string, object> parameters = null)
    {
        if (parameters != null)
            return Shell.Current.GoToAsync(route, parameters);

        return Shell.Current.GoToAsync(route);
            
    }
}
