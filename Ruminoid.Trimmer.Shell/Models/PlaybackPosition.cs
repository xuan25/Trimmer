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

        #region Constructors

        public PlaybackPosition()
        {

        }

        public PlaybackPosition(long time)
        {
            Time = time;
        }

        public PlaybackPosition(int minute, int second, int timeCode)
        {
            Minute = minute;
            Second = second;
            TimeCode = timeCode;
        }

        #endregion

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

        private long _total;

        public long Total
        {
            get => _total;
            set
            {
                _total = value;
                ConvertPercentage();
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

        private double _percentage;

        public double Percentage
        {
            get => _percentage;
            set
            {

            }
        }

        #endregion

        #region Display

        public string MinuteDisplay => Minute.ToString("D2");

        public string SecondDisplay => Second.ToString("D2");

        public string TimeCodeDisplay => TimeCode.ToString("D2");

        #endregion

        #region Converter

        private void ConvertToPosition()
        {
            _minute = (int) (_time / (1000 * 60));
            _second = (int) ((_time - _minute * 1000 * 60) / 1000);
            _timeCode = (int) ((_time - _minute * 1000 * 60 - _second * 1000) / 10);
            ConvertPercentage();
        }

        private void ConvertToTime()
        {
            _time = _minute * 1000 * 60 + _second * 1000 + _timeCode * 10;
            ConvertPercentage();
        }

        private void ConvertPercentage()
        {
            _percentage = Time / (double) Total;
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Time)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Total)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Minute)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Second)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TimeCode)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Percentage)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MinuteDisplay)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SecondDisplay)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TimeCodeDisplay)));
        }

        #endregion

    }
}
