using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ruminoid.Trimmer.Shell.Models
{
    public abstract class Modified : INotifyPropertyChanged
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
}
