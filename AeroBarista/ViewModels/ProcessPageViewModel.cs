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
    public class ProcessPageViewModel : BaseViewModel, INotifyPropertyChanged
    {
        // TODO: get data from parameter, not api client!
        IRecipeApiClient apiClient;
        IBrewProcessService timer;
        ProcessStateService processStateService;
        private RecipeModel _activeRecipe;
        public RecipeModel Recipe
        {
            get => _activeRecipe;
            set
            {
                if (_activeRecipe != value)
                {
                    _activeRecipe = value;
                    OnPropertyChanged(nameof(Recipe)); 
                }
            }
        }

        private RecipeStepModel _activeStep;
        public RecipeStepModel ActiveStep
        {
            get => _activeStep;
            set
            {
                if (_activeStep != value)
                {
                    _activeStep = value;
                    OnPropertyChanged(nameof(ActiveStep));
                }
            }
        }

        private TimeSpan currentTime;
        public TimeSpan CurrentTime
        {
            get => currentTime;
            set
            {
                if (currentTime != value)
                {
                    currentTime = value;
                    OnPropertyChanged(nameof(CurrentTime));
                }
            }
        }

        private TimeSpan remainingTime;
        public TimeSpan RemainingTime
        {
            get => remainingTime;
            set
            {
                if (remainingTime != value)
                {
                    remainingTime = value;
                    OnPropertyChanged(nameof(RemainingTime));
                }
            }
        }




        public ProcessPageViewModel(INavigationService navigationService, IRecipeApiClient apiClient, IBrewProcessService timer) : base(navigationService)
        {
            this.apiClient = apiClient;
            GetData();
            processStateService = new ProcessStateService(Recipe.Steps);
            _activeStep = processStateService.GetActiveStep();
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
        }

        private void StateChangeCallback(RecipeStepModel? current, RecipeStepModel? prev, RecipeStepModel? next)
        {
            if (current != null)
            {
                ActiveStep = current;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private async void GetData()
        {
            var x = await apiClient.GetAll();
            _activeRecipe = x.First();
        }


    }
}
