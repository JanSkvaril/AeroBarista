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
using System.ComponentModel.DataAnnotations;

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
        Error = string.Empty;
    }

    [Required(ErrorMessage = "Order is required")]
    [Range(1,100)]
    [ObservableProperty]
    private int order;

    [Required(ErrorMessage = "Short text is required", AllowEmptyStrings = false)]
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
    private int? time;

    [ObservableProperty]
    private string error;

    public CreateStepViewModel(INavigationService navigationService) : base(navigationService)
    {
        StepTypes = new(Enum.GetNames(typeof(StepType)));
        StepTypeIndex = 0;
    }

    [RelayCommand]
    public void Save()
    {
        ValidateAllProperties();

        if (HasErrors)
        {
            Error = string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
            return;
        }

        Error = string.Empty;

        TimeSpan? stepTime = null;
        if (Time != null && Time > 0)
        {
            stepTime = TimeSpan.FromSeconds((double)Time);
        }
        if (recipeStepToEdit != null)
        {
            SaveResult?.Invoke(recipeStepToEdit with { Order = Order, ShorText = ShortText, Description = Description, StepType = (StepType)StepTypeIndex, Value = Value, Time= stepTime });
            return;
        }
       
        RecipeStepModel step = new(-1, -1, Order, ShortText, Description, (StepType)StepTypeIndex, Value, stepTime);
        SaveResult?.Invoke(step);
    }
}
