using AeroBarista.Services.Interfaces;
using AeroBarista.ViewModels.Base;

namespace AeroBarista.ViewModels
{
    public partial class AddReviewViewModel : BaseViewModel
    {
        public AddReviewViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
