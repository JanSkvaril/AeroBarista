﻿using AeroBarista.Shared.Models;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace AeroBarista.ApiClients.Interfaces;

public interface IRecipeApiClient
{
    public Task<ICollection<RecipeModel>> GetAll();
    public Task Create(RecipeModel recipe);
    public Task<bool> Update(RecipeModel recipe);
    public Task<bool> Delete(int recipeId);
}
