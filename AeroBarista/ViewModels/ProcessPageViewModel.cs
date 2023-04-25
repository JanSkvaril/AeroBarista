using AeroBarista.ApiClients.Interfaces;
using AeroBarista.Attributes;
using AeroBarista.Models;
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
    public class ProcessPageViewModel : BaseViewModel
    {
        // TODO: get data from parameter, not api client!
        IRecipeApiClient apiClient;


        private RecipeModel _activeRecipe;
        public RecipeModel Recipe
        {
            get => _activeRecipe;
            set
            {
                if (_activeRecipe != value)
                {
                    _activeRecipe = value;
                    OnPropertyChanged(); // reports this property
                }
            }
        }

        public ProcessPageViewModel(INavigationService navigationService, IRecipeApiClient apiClient) : base(navigationService)
        {
            this.apiClient = apiClient;
            GetData();
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
