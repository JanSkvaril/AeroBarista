﻿using AeroBarista.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroBarista.Services.Interfaces
{
    public interface IProcessStateService
    {
        /// <summary>
        /// Inicialize state of the service
        /// </summary>
        /// <param name="fastStart">If true, find first step with time and set it as initial step</param>
        void Inicialize(bool fastStart = false);

        /// <summary>
        /// Callback is called every time state is changed
        /// </summary>
        /// <param name="callback">Parameters are [current, previous, next]. Any of them can be null</param>
        public void SetStateChangeCallback(Action<RecipeStepModel?, RecipeStepModel?, RecipeStepModel?> callback);

        public void SetFinishedCallback(Action callback);


        public RecipeStepModel? GetActiveStep();


        public bool IsFinished();


        public void UpdateState(TimeSpan currentTime);


        public TimeSpan GetRemainingTimeForCurrentStep(TimeSpan currentTime);

        public void NextStep();

        public void PrevStep();
    }
}
