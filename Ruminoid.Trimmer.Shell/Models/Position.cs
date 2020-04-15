using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ruminoid.Trimmer.Shell.Models
{
    public class Position : Modify
    {

        #region Constructors

        public Position()
        {

        }

        public Position(long time)
        {
            Time = time;
            IsModified = true;
        }

        public Position(int minute, int second, int timeCode)
        {
            Minute = minute;
            Second = second;
            TimeCode = timeCode;
            IsModified = true;
        }

        public Position(Position position)
        {
            Time = position.Time;
            IsModified = true;
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
                OnPositionPropertyChanged();
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
                OnPositionPropertyChanged();
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
                OnPositionPropertyChanged();
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
                OnPositionPropertyChanged();
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
                OnPositionPropertyChanged();
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

        #region Utilities

        private static (int, int) ConvertToHourMinute(int minute)
        {
            int hour = minute / 60;
            return (hour, minute - hour * 60);
        }

        public string ConvertToSubtitleTimestamp()
        {
            (int h, int m) = ConvertToHourMinute(Minute);
            return $"{h:D2}:{m:D2}:{Second:D2}.{TimeCode:D2}";
        }

        public int CalculateDelta(Position nextPosition) => (int) ((nextPosition.Time - Time) / 10);

        #endregion

        #region PropertyChanged

        protected virtual void OnPositionPropertyChanged()
        {
            OnPropertyChanged(nameof(Time));
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(Minute));
            OnPropertyChanged(nameof(Second));
            OnPropertyChanged(nameof(TimeCode));
            OnPropertyChanged(nameof(Percentage));
            OnPropertyChanged(nameof(MinuteDisplay));
            OnPropertyChanged(nameof(SecondDisplay));
            OnPropertyChanged(nameof(TimeCodeDisplay));
            IsModified = true;
        }

        #endregion

    }
}
