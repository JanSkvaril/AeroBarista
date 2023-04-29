using AeroBarista.Attributes;
using AeroBarista.Services.Interfaces;
using AeroBarista.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AeroBarista.ViewModels
{
    [ExportTransient]
    public partial class DetailRecipeViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string name;

        public DetailRecipeViewModel(INavigationService navigationService) : base(navigationService)
        {
            Name = "asscc";
        }
    }
}
