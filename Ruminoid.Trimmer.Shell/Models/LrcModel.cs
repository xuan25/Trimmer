using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ruminoid.Trimmer.Shell.Models
{

    public sealed class LrcModel : INotifyPropertyChanged
    {

        #region Current

        public static LrcModel Current { get; set; } = new LrcModel();

        #endregion

        #region DataContext

        private bool _modified;

        public bool Modified
        {
            get => _modified;
            set
            {
                _modified = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Items

        private ObservableCollection<LrcLine> _items = new ObservableCollection<LrcLine>()
        {
            new LrcLine()
            {
                Items = new ObservableCollection<LrcChar>()
                {
                    new LrcChar("你", new PlaybackPosition(0, 0, 0)),
                    new LrcChar("好", new PlaybackPosition(0, 1, 0)),
                    new LrcChar("，", new PlaybackPosition(0, 2, 0)),
                    new LrcChar("世", new PlaybackPosition(0, 3, 0)),
                    new LrcChar("界", new PlaybackPosition(0, 4, 0))
                }
            }
        };

        public ObservableCollection<LrcLine> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }

    public sealed class LrcLine : INotifyPropertyChanged
    {

        #region Items

        private ObservableCollection<LrcChar> _items = new ObservableCollection<LrcChar>();

        public ObservableCollection<LrcChar> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }

    public sealed class LrcChar : INotifyPropertyChanged
    {

        #region Constructors

        public LrcChar()
        {

        }

        public LrcChar(string chr, PlaybackPosition position)
        {
            Char = chr;
            Position = position;
        }

        #endregion

        #region DataContext

        private string _char = "";

        public string Char
        {
            get => _char;
            set
            {
                _char = value;
                OnPropertyChanged();
            }
        }

        private PlaybackPosition _position = new PlaybackPosition();

        public PlaybackPosition Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
