﻿using AeroBarista.ApiClients.Interfaces;
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
                    OnPropertyChanged(nameof(CurrentTimeText));
                }
            }
        }


        public string CurrentTimeText
        {
            get => currentTime.ToString(@"mm\:ss");
        }


        public ProcessPageViewModel(INavigationService navigationService, IRecipeApiClient apiClient, IBrewProcessService timer) : base(navigationService)
        {
            this.apiClient = apiClient;
            GetData();
            this.timer = timer;
            this.timer.RegisterTickCallback(TimeTickCallback);
            timer.Start();
        }

        private void TimeTickCallback(TimeSpan time)
        {
            CurrentTime = time;
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
