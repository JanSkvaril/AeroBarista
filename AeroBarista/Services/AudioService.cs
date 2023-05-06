using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroBarista.Services
{
    public class AudioService
    {

        public static async Task PlayFinishedSound()
        {
  
            var audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("finished_sound.mp3"));

            audioPlayer.Play();
           
        }

        public static async Task PlayNextStepSound()
        {
            var audioPlayer = AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("next_step_sound.mp3"));

            audioPlayer.Play();
        }
    }
}
