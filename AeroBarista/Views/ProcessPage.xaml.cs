using AeroBarista.Attributes;
using AeroBarista.ViewModels;

namespace AeroBarista.Views;


[ExportTransient]
public partial class ProcessPage : ContentPage
{
	public ProcessPage(ProcessPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

        SetupAnimations();
    }
	private void SetupAnimations()
	{
        CurrentStep.PropertyChanging += async (sender, e) =>
        {
            if (e.PropertyName == Label.TextProperty.PropertyName)
            {

                CurrentStep.Opacity = 0;
                await CurrentStep.FadeTo(1, 300);
            }
        };

        PrevStep.PropertyChanging += async (sender, e) =>
        {
            if (e.PropertyName == Label.TextProperty.PropertyName)
            {

                PrevStep.Opacity = 0;
                await PrevStep.FadeTo(1, 100);
            }
        };

        NextStep.PropertyChanging += async (sender, e) =>
        {
            if (e.PropertyName == Label.TextProperty.PropertyName)
            {

                NextStep.Opacity = 0;
                await NextStep.FadeTo(1, 400);
            }
        };
    }
}