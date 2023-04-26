﻿using AeroBarista.Models;
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
        /// Callback is called every time state is changed
        /// </summary>
        /// <param name="callback">Parameters are [current, previous, next]. Any of them can be null</param>
        public void SetStateChangeCallback(Action<RecipeStepModel?, RecipeStepModel?, RecipeStepModel?> callback);


        public RecipeStepModel? GetActiveStep();


        public bool IsFinished();


        public void UpdateState(TimeSpan currentTime);


        public TimeSpan GetRemainingTimeForCurrentStep(TimeSpan currentTime);

      
    }
}
