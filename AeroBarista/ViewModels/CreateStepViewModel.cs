using AeroBarista.Attributes;
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
    private int value;

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
        RecipeStepModel step = new(-1, -1, Order, ShortText, Description, (StepType)StepTypeIndex, Value, TimeSpan.FromSeconds(Time));
        SaveResult?.Invoke(step);
    }
}
