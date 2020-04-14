using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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

        public string SkipData =
            File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\SkipData.txt"));

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

        private LrcLine _selectedItem;

        public LrcLine SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnLrcModelPropertyChanged();
            }
        }

        private int _selectedIndex = -1;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
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
            foreach (string lyric in lyrics) AddLyric(lyric);
        }

        public void AddLyric(string lyric)
        {
            ObservableCollection<LrcChar> chars = new ObservableCollection<LrcChar>();
            foreach (char c in lyric)
            {
                if (c == '\r' || c == '\n' || c == '\0') continue;
                LrcChar lc = new LrcChar(c);
                foreach (char s in SkipData)
                    if (c == s)
                        lc.Skip = true;
                chars.Add(lc);
            }

            if (chars.Count == 0) return;
            chars.Add(new LrcChar(' ') {EndLine = true});
            Items.Add(new LrcLine {Items = chars});
        }

        #endregion

        #region Operations

        public void Apply(Position position)
        {

        }

        public void Undo()
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

        private LrcChar _selectedItem;

        public LrcChar SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnLrcLinePropertyChanged();
            }
        }

        private int _selectedIndex = -1;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
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

        public LrcChar(char chr)
        {
            Char = chr;
        }

        public LrcChar(char chr, Position position)
        {
            Char = chr;
            Position = position;
            IsModified = true;
        }

        #endregion

        #region DataContext

        private char _char;

        public char Char
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
