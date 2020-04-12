﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ruminoid.Trimmer.Shell.Models
{

    public sealed class LrcModel : Modify, INotifyPropertyChanged
    {

        #region Current

        public static LrcModel Current { get; set; } = new LrcModel();

        #endregion

        #region DataContext

        #endregion

        #region Items

        private ObservableCollection<LrcLine> _items = new ObservableCollection<LrcLine>()
        {
            new LrcLine()
            {
                Items = new ObservableCollection<LrcChar>()
                {
                    new LrcChar("你", new Position(0, 0, 0)),
                    new LrcChar("好", new Position(0, 1, 0)),
                    new LrcChar("，", new Position(0, 2, 0)),
                    new LrcChar("世", new Position(0, 3, 0)),
                    new LrcChar("界", new Position(0, 4, 0))
                }
            },
            new LrcLine()
            {
                Items = new ObservableCollection<LrcChar>()
                {
                    new LrcChar("再", new Position(0, 0, 0)),
                    new LrcChar("见", new Position(0, 1, 0)),
                    new LrcChar("，", new Position(0, 2, 0)),
                    new LrcChar("世", new Position(0, 3, 0)),
                    new LrcChar("界", new Position(0, 4, 0))
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

        public new event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            IsModified = true;
        }

        #endregion

    }

    public sealed class LrcLine : ModifyTarget, INotifyPropertyChanged
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
                OnPropertyChanged();
            }
        }

        #endregion

        #region PropertyChanged

        public new event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            IsModified = true;
        }

        #endregion

    }

    public sealed class LrcChar : ModifyTarget, INotifyPropertyChanged
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
                OnPropertyChanged();
            }
        }

        private Position _position = new Position();

        public Position Position
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

        public new event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            IsModified = true;
        }

        #endregion

    }
}
