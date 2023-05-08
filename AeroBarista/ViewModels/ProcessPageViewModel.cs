using AeroBarista.Attributes;
using AeroBarista.Shared.Models;
using AeroBarista.Services;
using AeroBarista.Services.Interfaces;
using AeroBarista.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AeroBarista.ViewModels
{

    [ExportTransient]
    [QueryProperty(nameof(Recipe), nameof(Recipe))]
    public partial class ProcessPageViewModel : BaseViewModel
    {
        IBrewProcessService timer;
        IProcessStateService processStateService;
        IAudioService audio;


        [ObservableProperty]
        private RecipeModel? recipe;

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

        [ObservableProperty]
        private bool isStepWithTime = false;

        [ObservableProperty]
        private bool isProcessPaused = false;

        public ProcessPageViewModel(INavigationService navigationService, IBrewProcessService timer, IAudioService audio) : base(navigationService)
        {
            this.timer = timer;
            this.timer.RegisterTickCallback(TimeTickCallback);
            this.audio = audio;
        }

        partial void OnRecipeChanged(RecipeModel value)
        {
            if (Recipe == null) return;
            timer.Stop();
            timer.Reset();  
            IsProcessPaused = false;
            CurrentTime = new TimeSpan();
            RemainingTime = new TimeSpan();

            processStateService = new ProcessStateService(Recipe.Steps); // TODO: use factory?

            processStateService.SetStateChangeCallback(StateChangeCallback);
            processStateService.SetFinishedCallback(FinishedCallback);
            processStateService.Inicialize();

        }

        public Task OnDisappearing()
        {

            Pause();

            return Task.CompletedTask;
        }

        private void TimeTickCallback(TimeSpan time)
        {
            CurrentTime = time;
            processStateService.UpdateState(time);
            RemainingTime = processStateService.GetRemainingTimeForCurrentStep(time);
            if (ActiveStep != null && ActiveStep.Time != null)
            {
                StepProgress = RemainingTime.TotalSeconds / ActiveStep.Time.Value.TotalSeconds;
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
                    
                IsStepWithTime = current.Time != null;
                // new current step has time and timer was not started
                if (ActiveStep != null && ActiveStep.Time != null && !timer.IsRunning()) 
                {
                    if (IsProcessPaused)
                    {
                        timer.Resume();
                    }
                    else
                    {
                        timer.Start();
                    }
                    IsProcessPaused = false;
                }
            }
            else
            {
                IsStepWithTime = false;
            }
            _ = audio.PlayNextStepSound();
        }

        private void FinishedCallback()
        {
            _ = audio.PlayFinishedSound();
            NavigationService.NavigateToAsync("FinishedPage");


        }

        [RelayCommand]
        public void GoToNextStep()
        {
            processStateService.NextStep();
        }

        [RelayCommand]
        public void GoToPrevStep()
        {
            processStateService.PrevStep();
        }

        [RelayCommand]
        public void Pause()
        {
            timer.Stop();
            IsProcessPaused = true;
        }

        [RelayCommand]
        public void Resume()
        {
            timer.Resume();
            IsProcessPaused = false;
        }
    }
}
