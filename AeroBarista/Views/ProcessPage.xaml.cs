using AeroBarista.Attributes;
using AeroBarista.ViewModels;

namespace AeroBarista.Views;


[ExportTransient]
public partial class ProcessPage : ContentPage
{
    private readonly ProcessPageViewModel viewModel;
    private readonly string pausePath = "pausesolid.png";
    private readonly string playPath = "playsolid.png";

    public ProcessPage(ProcessPageViewModel vm)
	{
		InitializeComponent();
        viewModel = vm;
		BindingContext = vm;
        SetupAnimations();
    }

    public void PausePlayButtonClicked(object sender, EventArgs args)
    {
        var isPaused = viewModel.IsPaused();
        if (!isPaused)
        {
            startPlayButton.Source = playPath;
            viewModel.Resume();
        }
        else
        {
            startPlayButton.Source = pausePath;
            viewModel.Pause();
        }
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