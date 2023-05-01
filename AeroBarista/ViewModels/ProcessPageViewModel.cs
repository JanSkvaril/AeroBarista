using AeroBarista.ApiClients.Interfaces;
using AeroBarista.Attributes;
using AeroBarista.Models;
using AeroBarista.Services;
using AeroBarista.Services.Interfaces;
using AeroBarista.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AeroBarista.ViewModels
{

    [ExportTransient]
    [QueryProperty(nameof(Recipe), nameof(Recipe))]
    public partial class ProcessPageViewModel : BaseViewModel
    {
        IBrewProcessService timer;
        IProcessStateService processStateService;


        [ObservableProperty]
        private RecipeModel recipe;

        [ObservableProperty]
        private RecipeStepModel? prevStep;
        [ObservableProperty]
        private RecipeStepModel? activeStep;
        [ObservableProperty]
        private RecipeStepModel? nextStep;


        [ObservableProperty]
        private TimeSpan currentTime;


        [ObservableProperty]
        private TimeSpan remainingTime;

        [ObservableProperty]
        private double stepProgress;

        public ProcessPageViewModel(INavigationService navigationService, IBrewProcessService timer) : base(navigationService)
        {
            this.timer = timer;
            this.timer.RegisterTickCallback(TimeTickCallback);
        }

        partial void OnRecipeChanged(RecipeModel value)
        {
            processStateService = new ProcessStateService(Recipe.Steps);

            processStateService.SetStateChangeCallback(StateChangeCallback);
            processStateService.Inicialize();
            timer.Start();
        }

        private void TimeTickCallback(TimeSpan time)
        {
            CurrentTime = time;
            processStateService.UpdateState(time);
            RemainingTime = processStateService.GetRemainingTimeForCurrentStep(time);
            if (ActiveStep != null)
            {
                StepProgress = RemainingTime.TotalSeconds / ActiveStep.time.Value.TotalSeconds;
            }
            else StepProgress = 0.0;
        }

        private void StateChangeCallback(RecipeStepModel? current, RecipeStepModel? prev, RecipeStepModel? next)
        {
            if (current != null)
            {
                ActiveStep = current;
                PrevStep = prev;
                NextStep = next;
            }
        }
    }
}
