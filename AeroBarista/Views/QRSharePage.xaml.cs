using AeroBarista.Attributes;
using AeroBarista.ViewModels;
using Camera.MAUI;
using System.Diagnostics;

namespace AeroBarista.Views;


[ExportTransient]
public partial class QRSharePage : ContentPage
{
	public QRSharePage(QRSharePageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;

       // QrImage.Source = ImageSource.FromStream(() => new MemoryStream(vm.QrCodeBytes));
        vm.PropertyChanged += async (sender, e) =>
        {
            if (e.PropertyName == nameof(vm.QrCodeBytes))
            {
                QrImage.Source = ImageSource.FromStream(() => new MemoryStream(vm.QrCodeBytes));
            }
       
        };

    }


}