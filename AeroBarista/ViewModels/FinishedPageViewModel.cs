using AeroBarista.Attributes;
using AeroBarista.Services.Interfaces;
using AeroBarista.ViewModels.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;




namespace AeroBarista.ViewModels
{
    [ExportTransient]
    public partial class FinishedPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        private FileResult? takenImage;
        ICameraService cameraService;
        INativeShareService shareService;


        public FinishedPageViewModel(INavigationService navigationService, ICameraService cameraService, INativeShareService shareService) : base(navigationService)
        {
            this.cameraService = cameraService;
            this.shareService = shareService;
        }

        [RelayCommand]
        public void TakePhoto()
        {
            ObtainPhoto();
        }

        [RelayCommand]
        public void SharePhoto()
        {
            ShareImage();   
        }

        public void ShareImage()
        {
            if (TakenImage == null) return;
            shareService.ShareFile("Share your image", TakenImage);


        }

        public async void ObtainPhoto()
        {
            TakenImage = await cameraService.ObtainPhoto();
         
        }
    }
}
