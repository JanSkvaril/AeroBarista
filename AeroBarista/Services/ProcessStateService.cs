using AeroBarista.Attributes;
using AeroBarista.Models;
using AeroBarista.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroBarista.Services
{
    [ExportTransientAs(nameof(IProcessStateService))]
    public class ProcessStateService : IProcessStateService
    {
        IList<RecipeStepModel> steps;
        Action<RecipeStepModel?, RecipeStepModel?, RecipeStepModel?>? stepChangeCallback = null;
        Action? finishedCallback = null;
        RecipeStepModel? activeStep = null;
        TimeSpan stepStartTime;
        TimeSpan lastCurrentTime;
        private bool goToNextStepRequest = false;

        public ProcessStateService(IList<RecipeStepModel> steps) 
        { 
            this.steps = steps;
            if (steps == null || steps.Count == 0)
            {
                throw new ArgumentException("Steps must contain at least one step");
            }
            
        }

        public void Inicialize()
        {
            ChangeState(steps.First(),new TimeSpan());
        }

        /// <summary>
        /// Callback is called every time state is changed
        /// </summary>
        /// <param name="callback">Parameters are [current, previous, next]. Any of them can be null</param>
        public void SetStateChangeCallback(Action<RecipeStepModel?, RecipeStepModel?, RecipeStepModel?> callback)
        {
            stepChangeCallback = callback;
        }

        public void SetFinishedCallback(Action callback)
        {
            finishedCallback = callback;
        }

        public RecipeStepModel? GetActiveStep()
        {
            return activeStep;
        }

        public bool IsFinished()
        {
            return activeStep == null;
        }

        public void UpdateState(TimeSpan currentTime)
        {
            lastCurrentTime = currentTime;
            if (steps.Count() == 0) return;
            if (activeStep == null) return;
            if (activeStep.time == null) return;

            else if (stepStartTime + activeStep.time < currentTime)
            {
                if (activeStep == steps.Last())
                {
                    ChangeState(null, currentTime);
                    if (finishedCallback != null) finishedCallback();
                }
                else
                {
                    int current_index = steps.IndexOf(activeStep);
                    ChangeState(steps[current_index + 1], currentTime);
                }
            }
        }

        public TimeSpan GetRemainingTimeForCurrentStep(TimeSpan currentTime)
        {
            if (activeStep == null) return TimeSpan.Zero;   
            if (activeStep.time == null) return TimeSpan.Zero;
            return (TimeSpan)(stepStartTime + activeStep.time - currentTime);
        }

        private void ChangeState(RecipeStepModel? newStep, TimeSpan startTime)
        {
            activeStep = newStep;
            stepStartTime = new TimeSpan(0,0, (int)startTime.TotalSeconds);
            if (stepChangeCallback != null)
            {
                if (activeStep == null)
                {
                    stepChangeCallback.Invoke(null, steps.Last(), null);
                }
                else
                {
                    RecipeStepModel? prevStep = null;
                    RecipeStepModel? nextStep = null;
                    int index = steps.IndexOf(activeStep);
                    if (activeStep != steps.First()) prevStep = steps[index - 1];
                    if (activeStep != steps.Last()) nextStep = steps[index + 1];
                    stepChangeCallback.Invoke(activeStep, prevStep, nextStep);
                }
            }
        }

        public void NextStep()
        {
            if (activeStep == null) return;
            if (activeStep == steps.Last())
            {
                ChangeState(null, lastCurrentTime);
                if (finishedCallback != null) finishedCallback();
            }
            else
            {
                int current_index = steps.IndexOf(activeStep);
                ChangeState(steps[current_index + 1], lastCurrentTime);
            }
        }

        public void PrevStep()
        {
            if (activeStep == null) return;
            if (activeStep == steps.First())
            {
                ChangeState(steps.First(), lastCurrentTime); // start again
            }
            else
            {
                int current_index = steps.IndexOf(activeStep);
                ChangeState(steps[current_index - 1], lastCurrentTime);
            }
        }

    }
}
