using AeroBarista.Attributes;
using AeroBarista.Services.Interfaces;
using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroBarista.Services
{
    [ExportTransientAs(nameof(IAudioService))]
    public class AudioService : IAudioService
    {

        public async Task PlayFinishedSound()
        {
  
            var audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("finished_sound.mp3"));

            audioPlayer.Play();
           
        }

        public async Task PlayNextStepSound()
        {
            var audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("next_step_sound.mp3"));

            audioPlayer.Play();
        }
    }
}
