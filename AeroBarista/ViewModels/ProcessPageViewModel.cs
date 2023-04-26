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
    public partial class ProcessPageViewModel : BaseViewModel
    {
        // TODO: get data from parameter, not api client!
        IRecipeApiClient apiClient;
        IBrewProcessService timer;
        IProcessStateService processStateService;


        [ObservableProperty]
        RecipeModel recipe;

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



        public ProcessPageViewModel(INavigationService navigationService, IRecipeApiClient apiClient, IBrewProcessService timer) : base(navigationService)
        {
            this.apiClient = apiClient;
            GetData();
            processStateService = new ProcessStateService(Recipe.Steps);
            activeStep = processStateService.GetActiveStep();

            processStateService.SetStateChangeCallback(StateChangeCallback);
            this.timer = timer;
            this.timer.RegisterTickCallback(TimeTickCallback);
            timer.Start();
        }

        private void TimeTickCallback(TimeSpan time)
        {
            CurrentTime = time;
            processStateService.UpdateState(time);
            RemainingTime = processStateService.GetRemainingTimeForCurrentStep(time);
            if (ActiveStep != null)
            {
                StepProgress = RemainingTime.TotalSeconds / ActiveStep.time.TotalSeconds;
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


        private async void GetData()
        {
            var x = await apiClient.GetAll();
            Recipe = x.First();
        }


    }
}
