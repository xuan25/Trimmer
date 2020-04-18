using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MetroRadiance.UI;
using Ruminoid.Trimmer.Shell.Models;

namespace Ruminoid.Trimmer.Shell.Views
{
    public partial class PlaybackView : INotifyPropertyChanged
    {

        #region PlaybackControl

        private bool _mediaLoaded;

        public bool MediaLoaded
        {
            get => _mediaLoaded;
            set
            {
                _mediaLoaded = value;
                OnPropertyChanged();
            }
        }

        public Position Position { get; } = new Position();

        private bool _playing;

        public bool Playing
        {
            get => _playing;
            set
            {
                if (!MediaLoaded) return;
                _playing = value;
                if (value)
                    VideoElement.Play();
                else
                    VideoElement.Pause();
                if (value) ThemeService.Current.ChangeAccent(Accent.Orange);
                else ThemeService.Current.ChangeAccent(Accent.Blue);
                OnPropertyChanged();
            }
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
