using AeroBarista.Attributes;
using AeroBarista.ViewModels;
using Camera.MAUI;
using Camera.MAUI.ZXingHelper;

namespace AeroBarista.Views;

[ExportTransient]
public partial class ImportPage : ContentPage
{

    ImportPageViewModel viewmodel;
    public ImportPage(ImportPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        viewmodel = vm;
       
        cameraView.CamerasLoaded += CameraView_CamerasLoaded;
        cameraView.BarcodeDetected += CameraView_BarcodeDetected;

        cameraView.BarCodeOptions = new Camera.MAUI.ZXingHelper.BarcodeDecodeOptions
        {
            AutoRotate = true,
            PossibleFormats = { ZXing.BarcodeFormat.QR_CODE },
            ReadMultipleCodes = false,
            TryHarder = true,
            TryInverted = true
        };
        cameraView.BarCodeDetectionFrameRate = 10;
        cameraView.BarCodeDetectionEnabled = true;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await cameraView.StartCameraAsync();
            cameraView.IsVisible = true;
        });
      
    }
    protected override void OnDisappearing()
    {
       
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await cameraView.StopCameraAsync();
            cameraView.IsVisible = false;
        });
        
        base.OnDisappearing();
    }

    private void CameraView_CamerasLoaded(object sender, EventArgs e)
    {
        if (cameraView.NumCamerasDetected > 0)
        {
            if (cameraView.NumMicrophonesDetected > 0)
                cameraView.Microphone = cameraView.Microphones.First();
            cameraView.Camera = cameraView.Cameras.First();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (await cameraView.StartCameraAsync() == CameraResult.Success)
                {
                   
                }
            });
        }
    }

    private void CameraView_BarcodeDetected(object sender, BarcodeEventArgs args)
    {
        string text = args.Result[0].Text;
     
        MainThread.BeginInvokeOnMainThread(() =>
        {
            _ = viewmodel.QRDetected(text);
        });
    }
}