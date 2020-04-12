using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ruminoid.Trimmer.Shell.Models
{

    public abstract class Modify : INotifyPropertyChanged
    {

        private bool _isModified;

        public bool IsModified
        {
            get => _isModified;
            set
            {
                _isModified = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsModified)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }

    public abstract class ModifyTarget : Modify
    {

        private bool _isTargeting;

        public bool IsTargeting
        {
            get => _isTargeting;
            set
            {
                _isTargeting = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsTargeting)));
            }
        }

        private bool _isCompleted;

        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCompleted)));
            }
        }

        public new event PropertyChangedEventHandler PropertyChanged;

    }

}
