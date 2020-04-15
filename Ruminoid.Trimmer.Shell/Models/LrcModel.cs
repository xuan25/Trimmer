using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MS.WindowsAPICodePack.Internal;

namespace Ruminoid.Trimmer.Shell.Models
{

    public sealed partial class LrcModel : Modify
    {

        #region Current

        public static LrcModel Current { get; set; } = new LrcModel();

        public static readonly string SkipData =
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

        //private LrcLine _selectedItem;

        //public LrcLine SelectedItem
        //{
        //    get => _selectedItem;
        //    set
        //    {
        //        _selectedItem = value;
        //        OnLrcModelPropertyChanged();
        //    }
        //}

        //private int _selectedIndex = -1;

        //public int SelectedIndex
        //{
        //    get => _selectedIndex;
        //    set
        //    {
        //        _selectedIndex = value;
        //        OnLrcModelPropertyChanged();
        //    }
        //}

        #endregion

        #region GlobalIndex

        private int _globalIndex = -1;

        public int GlobalIndex
        {
            get => _globalIndex;
            set
            {
                _globalIndex = value;
                ClearTargeting?.Invoke();
                OnLrcModelPropertyChanged();
                (LrcChar c, LrcLine l) = GetCharAndLine(_globalIndex);
                if (c is null || l is null) return;
                c.IsTargeting = true;
                l.IsTargeting = true;
                SetTargeting?.Invoke(l);
            }
        }

        public int MeasureLineIndex(LrcLine line)
        {
            if (line is null) throw new ArgumentNullException(nameof(line), @"Line cannot be null");
            int index = -1;
            foreach (LrcLine lrcLine in Items)
            {
                if (lrcLine == line)
                    return index + 1;
                index += lrcLine.Items.Count;
            }

            return -1;
        }

        public LrcChar GetChar() => GetChar(GlobalIndex);

        public LrcChar GetChar(int index)
        {
            int i = -1;
            foreach (LrcLine line in Items)
            {
                if (i + line.Items.Count >= index)
                {
                    int d = index - i - 1;
                    return d < 0 ? null : line.Items[d];
                }

                i += line.Items.Count;
            }

            return null;
        }

        public (LrcChar, LrcLine) GetCharAndLine(int index)
        {
            int i = -1;
            foreach (LrcLine line in Items)
            {
                if (i + line.Items.Count >= index)
                {
                    int d = index - i - 1;
                    return d < 0 ? (null, null) : (line.Items[d], line);
                }

                i += line.Items.Count;
            }

            return (null, null);
        }

        #endregion

        #region Events

        public delegate void ClearTargetingHandler();

        public event ClearTargetingHandler ClearTargeting;

        public delegate void SetTargetingHandler(LrcLine lrcLine);

        public event SetTargetingHandler SetTargeting;

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
            bool resetIndex = Items.Count == 0;
            foreach (string lyric in lyrics) AddLyric(lyric);
            if (resetIndex) GlobalIndex = 0;
        }

        public void AddLyric(string lyric)
        {
            LrcLine line = new LrcLine(lyric);
            if (line.Items.Count <= 1) return;
            Items.Add(line);
        }

        #endregion

        #region Operations

        public void Apply(Position position)
        {
            LrcChar chr = GetChar();
            if (chr is null) return;
            chr.Position = position;
            chr.IsCompleted = true;
            int delta = 1;
            while (true)
            {
                LrcChar c = GetChar(GlobalIndex + delta);
                if (c is null) break;
                if (c.Skip) delta++;
                else break;
            }

            GlobalIndex += delta;
        }

        public void Undo()
        {
            int delta = 1;
            while (true)
            {
                LrcChar c = GetChar(GlobalIndex - delta);
                if (c is null) break;
                if (c.Skip) delta++;
                else break;
            }

            LrcChar chr = GetChar(GlobalIndex - delta);
            if (chr is null) return;
            chr.Position = new Position();
            chr.IsCompleted = false;
            GlobalIndex -= delta;
        }

        public void Skip()
        {

        }

        public void Break()
        {

        }

        public void ResetLineData(LrcLine line, string data)
        {
            int mIndex = MeasureLineIndex(line);
            int oldCount = line.Items.Count;
            bool decreaseIndex = mIndex < GlobalIndex;
            if (mIndex == -1) decreaseIndex = false;
            line.ResetData(data);
            if (decreaseIndex) GlobalIndex = GlobalIndex - oldCount + line.Items.Count;
        }

        public void RemoveLine(LrcLine line)
        {
            int mIndex = MeasureLineIndex(line);
            int oldCount = line.Items.Count;
            bool decreaseIndex = mIndex < GlobalIndex;
            if (mIndex == -1) decreaseIndex = false;
            Current.Items.Remove(line);
            if (decreaseIndex) GlobalIndex -= oldCount;
        }

        #endregion

    }

    public sealed class LrcLine : ModifyTarget
    {

        #region Constructors

        public LrcLine()
        {
            Origin = "";
            LrcModel.Current.ClearTargeting += OnClearTargeting;
        }

        public LrcLine(string lyric)
        {
            LrcModel.Current.ClearTargeting += OnClearTargeting;
            ResetData(lyric);
        }

        private void OnClearTargeting() => IsTargeting = false;

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

        //private LrcChar _selectedItem;

        //public LrcChar SelectedItem
        //{
        //    get => _selectedItem;
        //    set
        //    {
        //        _selectedItem = value;
        //        OnLrcLinePropertyChanged();
        //    }
        //}

        //private int _selectedIndex = -1;

        //public int SelectedIndex
        //{
        //    get => _selectedIndex;
        //    set
        //    {
        //        _selectedIndex = value;
        //        OnLrcLinePropertyChanged();
        //    }
        //}

        #endregion

        public string Origin { get; set; }

        public void ResetData(string lyric)
        {
            Origin = lyric.Replace("\r", "").Replace("\n", "");
            if (Items is null) Items = new ObservableCollection<LrcChar>();
            else Items.Clear();
            foreach (char c in lyric)
            {
                if (c == '\r' || c == '\n' || c == '\0') continue;
                if (c == '\\')
                {
                    Items.Add(new LrcChar(' ') { EndLine = true });
                    continue;
                }
                LrcChar lc = new LrcChar(c);
                foreach (char s in LrcModel.SkipData)
                    if (c == s)
                        lc.Skip = true;
                Items.Add(lc);
            }
        }

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
            LrcModel.Current.ClearTargeting += OnClearTargeting;
        }

        public LrcChar(char chr)
        {
            LrcModel.Current.ClearTargeting += OnClearTargeting;
            Char = chr;
        }

        public LrcChar(char chr, Position position)
        {
            LrcModel.Current.ClearTargeting += OnClearTargeting;
            Char = chr;
            Position = position;
            IsModified = true;
        }

        private void OnClearTargeting() => IsTargeting = false;

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
