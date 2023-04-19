using AeroBarista.Models;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace AeroBarista.ApiClients.Interfaces;

public interface IRecipeApiClient
{
    public Task<ICollection<RecipeModel>> GetAll();
    public Task Create(RecipeModel item);
    public Task<bool> Update(RecipeModel item);
    public Task<bool> Delete(RecipeModel item);
}
