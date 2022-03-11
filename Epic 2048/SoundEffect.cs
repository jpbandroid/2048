using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using Windows.ApplicationModel;
using Windows.Storage.Streams;
using Windows.Media.Playback;
using Windows.Media.Core;
using Windows.UI.Core;

namespace Epic_2048
{
    public enum SoundEfxEnum
    {
        MOVETILE,
        MOVETILE2,
        MERGETILE,
    }

    public class SoundEffect
    {
        private Dictionary<SoundEfxEnum, MediaElement> effects;

        public SoundEffect()
        {
            effects = new Dictionary<SoundEfxEnum, MediaElement>();
            LoadEfx();
        }

        private async void LoadEfx()
        {
            effects.Add(SoundEfxEnum.MOVETILE, await LoadSoundFile("MoveTile.mp3"));
            effects.Add(SoundEfxEnum.MOVETILE2, await LoadSoundFile("MoveTile2.mp3"));
            effects.Add(SoundEfxEnum.MERGETILE, await LoadSoundFile("MergeTile.mp3"));
        }

        private async Task<MediaElement> LoadSoundFile(string v)
        {
            MediaElement snd = new MediaElement();

            snd.AutoPlay = false;
            StorageFolder folder = await Package.Current.InstalledLocation.GetFolderAsync("Assets\\Sounds");
            StorageFile file = await folder.GetFileAsync(v);
            var stream = await file.OpenAsync(FileAccessMode.Read);
            snd.SetSource(stream, file.ContentType);
            return snd;
        }

        // Play a sound effect
        //App.GameSounds.Play(SoundEfxEnum.MERGETILE);
        // Or asynchronously
        //await App.GameSounds.Play(SoundEfxEnum.MERGETILE);
        public async Task Play(SoundEfxEnum efx)
        {
            var mediaElement = effects[efx];

            await mediaElement.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                mediaElement.Stop();
                mediaElement.Play();
            });
        }
    }
}
