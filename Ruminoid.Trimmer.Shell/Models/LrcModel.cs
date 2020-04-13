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

    public sealed class LrcModel : Modify
    {

        #region Current

        public static LrcModel Current { get; set; } = new LrcModel();

        #endregion

        #region DataContext

        #endregion

        #region Items

        private ObservableCollection<LrcLine> _items = new ObservableCollection<LrcLine>();

        public ObservableCollection<LrcLine> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnLrcModelPropertyChanged();
            }
        }

        #endregion

        #region PropertyChanged

        private void OnLrcModelPropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(propertyName);
            IsModified = true;
        }

        #endregion

        #region ItemOperations

        public void AddLyrics(string lyrics)
        {
            AddLyrics(lyrics.Split('\n'));
        }

        public void AddLyrics(string[] lyrics)
        {

        }

        #endregion

    }

    public sealed class LrcLine : ModifyTarget
    {

        #region Constructors

        public LrcLine()
        {

        }

        #endregion

        #region Items

        private ObservableCollection<LrcChar> _items = new ObservableCollection<LrcChar>();

        public ObservableCollection<LrcChar> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnLrcLinePropertyChanged();
            }
        }

        #endregion

        #region PropertyChanged

        private void OnLrcLinePropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(propertyName);
            IsModified = true;
        }

        #endregion

    }

    public sealed class LrcChar : ModifyTarget
    {

        #region Constructors

        public LrcChar()
        {

        }

        public LrcChar(string chr)
        {
            Char = chr;
        }

        public LrcChar(string chr, Position position)
        {
            Char = chr;
            Position = position;
            IsModified = true;
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
                OnLrcCharPropertyChanged();
            }
        }

        private Position _position = new Position();

        public Position Position
        {
            get => _position;
            set
            {
                _position = value;
                OnLrcCharPropertyChanged();
            }
        }

        private bool _endLine;

        public bool EndLine
        {
            get => _endLine;
            set
            {
                _endLine = value;
                OnLrcCharPropertyChanged();
            }
        }

        private bool _skip;

        public bool Skip
        {
            get => _skip;
            set
            {
                _skip = value;
                OnLrcCharPropertyChanged();
            }
        }

        #endregion

        #region PropertyChanged

        private void OnLrcCharPropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(propertyName);
            IsModified = true;
        }

        #endregion

    }
}
