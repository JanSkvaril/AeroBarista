﻿using AeroBarista.ApiClients.Interfaces;
using AeroBarista.Attributes;
using AeroBarista.Services;
using AeroBarista.Services.Interfaces;
using AeroBarista.Shared.Enums;
using AeroBarista.Shared.Models;
using AeroBarista.ViewModels.Base;
using AeroBarista.Views;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AeroBarista.ViewModels;

[ExportTransient]
[QueryProperty(nameof(Recipe), nameof(Recipe))]
public partial class CreateRecipeViewModel : BaseViewModel
{
    public delegate void DisplayPopupAction(Popup popup, int? editStepIndex = null);
    public event DisplayPopupAction DisplayPopup;

    private readonly IRecipeApiClient recipeApiClient;
    private readonly IRecipeStepApiClient recipeStepApiClient;
    private readonly Func<CreateStepPopUp> createStepPopUpFactory;
    private readonly CurrentDataProvider currentDataProvider;
    [ObservableProperty]
    private string headText = "Create Recipe";

    [ObservableProperty]
    private RecipeModel? recipe;

    [Required(ErrorMessage = "Name is required", AllowEmptyStrings = false)]
    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string description;

    public List<string> Methods { get; set; }
    [ObservableProperty]
    private int methodIndex;

    public List<string> Categories { get; set; }
    [ObservableProperty]
    private int categoryIndex;

    public List<string> GrandSizes { get; set; }
    [ObservableProperty]
    private int grandSizeIndex;

    [Required(ErrorMessage = "Coffee grams are required", AllowEmptyStrings = false)]
    [Range(1,1000)]
    [ObservableProperty]
    private int coffeeGrams;

    [ObservableProperty]
    private string author;

    [Required(ErrorMessage = "Water grams are required", AllowEmptyStrings = false)]
    [Range(1, 5000)]
    [ObservableProperty]
    private int totalWaterGrams;

    [ObservableProperty]
    private IList<RecipeStepModel> steps;

    [ObservableProperty]
    private string error;


    public CreateRecipeViewModel(INavigationService navigationService, IRecipeApiClient recipeApiClient, IRecipeStepApiClient recipeStepApiClient, Func<CreateStepPopUp> createStepPopUpFactory, CurrentDataProvider currentDataProvider) : base(navigationService)
    {
        Methods = new(Enum.GetNames(typeof(RecipeMethod)));
        Categories = new(Enum.GetNames(typeof(RecipeCategory)));
        GrandSizes = new(Enum.GetNames(typeof(GrandSize)));
        Steps = new ObservableCollection<RecipeStepModel>();

        this.recipeApiClient = recipeApiClient;
        this.recipeStepApiClient = recipeStepApiClient;
        this.createStepPopUpFactory = createStepPopUpFactory;
        this.currentDataProvider = currentDataProvider;
        MethodIndex = 0;
        CategoryIndex = 0;
        GrandSizeIndex = 0;
        Error = string.Empty;
    }

    partial void OnRecipeChanged(RecipeModel? value)
    {
        if (value == null)
            return;

        HeadText = "Update Recipe";
        Name = value.Name;
        Description = value.Description;
        MethodIndex = (int)value.Method;
        CategoryIndex = (int)value.Category;
        GrandSizeIndex = (int)value.GrandSize;
        CoffeeGrams = value.CoffeeGrams;
        Author = value.Author;
        TotalWaterGrams = value.TotalWaterGrams;
        Steps = value.Steps.OrderBy(s => s.Order).ToObservableCollection();
    }

        [RelayCommand]
    public void AddStep()
    {
        var createStepPopUp = createStepPopUpFactory();
        DisplayPopup?.Invoke(createStepPopUp);
    }

    [RelayCommand]
    public void UpdateStep(RecipeStepModel step)
    {
        currentDataProvider.CurrentRecipe = recipe;
        currentDataProvider.CurrentRecipeStep = step;

        var createStepPopUp = createStepPopUpFactory();
        DisplayPopup?.Invoke(createStepPopUp, steps.IndexOf(step));
    }

    public void OnStepsChanged()
    {
        OnPropertyChanged(nameof(Steps));
        Steps = Steps.OrderBy(s => s.Order).ToObservableCollection();
    }

    [RelayCommand]
    public void DeleteStep(RecipeStepModel step)
    {
        Steps.Remove(step);
    }


    [RelayCommand]
    public async void CreateRecipe()
    {
        ValidateAllProperties();

        if(HasErrors)
        {
            Error = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
            return;
        }

        Error = string.Empty;

        if (Recipe == null)
        {
            RecipeModel recipeData = new(-1, Name, Description, (RecipeMethod)MethodIndex, (RecipeCategory)CategoryIndex, (GrandSize)GrandSizeIndex, CoffeeGrams, Author, TotalWaterGrams, false, new(), null);
            var recipe = await recipeApiClient.Create(recipeData);
            recipe.Steps.Clear();

            foreach(var stepData in Steps)
            {
                var stepRecord = stepData with { RecipeId = recipe.Id };
                recipe.Steps.Add(await recipeStepApiClient.Create(stepRecord));
            }
            
            await recipeApiClient.Update(recipe);
        }
        else
        {
            Recipe = Recipe with { Name = Name, Description = Description, Method = (RecipeMethod)MethodIndex, Category = (RecipeCategory)CategoryIndex, CoffeeGrams = CoffeeGrams, Author = Author, TotalWaterGrams = TotalWaterGrams, Steps = Steps.ToList() };
            await recipeApiClient.Update(Recipe);
        }

        await NavigationService.NavigateToAsync("//RecipesPage");
    }
}
