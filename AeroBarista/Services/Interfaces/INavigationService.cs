using CommunityToolkit.Maui.Views;
using System.Runtime.InteropServices;

namespace AeroBarista.Services.Interfaces;

public interface INavigationService
{
    Task NavigateToAsync(string route, IDictionary<string, object> parameters = null);
}
