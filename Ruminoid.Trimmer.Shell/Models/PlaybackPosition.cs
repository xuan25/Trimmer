using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ruminoid.Trimmer.Shell.Models
{
    public class PlaybackPosition: INotifyPropertyChanged
    {

        #region DataContext

        private long _time;

        public long Time
        {
            get => _time;
            set
            {
                _time = value;
                ConvertToPosition();
                OnPropertyChanged();
            }
        }

        private int _minute;

        public int Minute
        {
            get => _minute;
            set
            {
                _minute = value;
                ConvertToTime();
                OnPropertyChanged();
            }
        }

        private int _second;

        public int Second
        {
            get => _second;
            set
            {
                _second = value;
                ConvertToTime();
                OnPropertyChanged();
            }
        }

        private int _timeCode;

        public int TimeCode
        {
            get => _timeCode;
            set
            {
                _timeCode = value;
                ConvertToTime();
                OnPropertyChanged();
            }
        }

        #endregion

        #region Converter

        private void ConvertToPosition()
        {
            _minute = (int) (_time / (1000 * 60));
            _second = (int) ((_time - _minute * 1000 * 60) / 1000);
            _timeCode = (int) ((_time - _minute * 1000 * 60 - _second * 1000) / 10);
        }

        private void ConvertToTime()
        {
            _time = _minute * 1000 * 60 + _second * 1000 + _timeCode * 10;
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
