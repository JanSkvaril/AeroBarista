using AeroBarista.ApiClients.Interfaces;
using AeroBarista.Attributes;
using AeroBarista.Services;
using AeroBarista.Services.Interfaces;
using AeroBarista.Shared.Enums;
using AeroBarista.Shared.Models;
using AeroBarista.ViewModels.Base;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AeroBarista.ViewModels;

[ExportTransient]
public partial class CreateStepViewModel : BaseViewModel
{
    public delegate void SaveAction(RecipeStepModel recipeStepModel);
    public event SaveAction SaveResult;

    private readonly CurrentDataProvider currentDataProvider;

    private RecipeStepModel recipeStepToEdit;

    public CreateStepViewModel(INavigationService navigationService, CurrentDataProvider currentDataProvider) : base(navigationService)
    {
        this.currentDataProvider = currentDataProvider;

        StepTypes = new(Enum.GetNames(typeof(StepType)));
    }

    public void OnOpen()
    {
        var recipeStep = currentDataProvider.CurrentRecipeStep;

        if (recipeStep == null)
            return;

        recipeStepToEdit = recipeStep;

        Order = recipeStep.Order;
        ShortText = recipeStep.ShorText;
        Description = recipeStep.Description;
        StepTypeIndex = (int)recipeStep.StepType;
        Value = recipeStep.Value;
        if (recipeStep.Time == null)
            Time = 0;
        else
            Time = Convert.ToInt32(((TimeSpan)recipeStep.Time!).TotalSeconds);
    }

    [ObservableProperty]
    private int order;

    [ObservableProperty]
    private string shortText;

    [ObservableProperty]
    private string description;

    public List<string> StepTypes { get; set; }
    [ObservableProperty]
    private int stepTypeIndex;

    [ObservableProperty]
    private int? value;

    [ObservableProperty]
    private int time;

    public CreateStepViewModel(INavigationService navigationService) : base(navigationService)
    {
        StepTypes = new(Enum.GetNames(typeof(StepType)));
        StepTypeIndex = 0;
    }

    [RelayCommand]
    public void Save()
    {
        if (recipeStepToEdit != null)
        {
            SaveResult?.Invoke(recipeStepToEdit with { Order = Order, ShorText = ShortText, Description = Description, StepType = (StepType)StepTypeIndex, Value = Value, Time = TimeSpan.FromSeconds(Time) });
            return;
        }

        RecipeStepModel step = new(-1, -1, Order, ShortText, Description, (StepType)StepTypeIndex, Value, TimeSpan.FromSeconds(Time));
        SaveResult?.Invoke(step);
    }
}
