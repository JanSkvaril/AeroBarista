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

        viewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(viewModel.IsProcessPaused))
            {
                var isPaused = viewModel.IsProcessPaused;
                if (isPaused)
                {
                    startPlayButton.Source = playPath;

                }
                else
                {
                    startPlayButton.Source = pausePath;
                }
            }
            return;
        };
    }

    public void PausePlayButtonClicked(object sender, EventArgs args)
    {
        var isPaused = viewModel.IsProcessPaused;
        if (isPaused)
        {
            viewModel.Resume();
        }
        else
        {
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