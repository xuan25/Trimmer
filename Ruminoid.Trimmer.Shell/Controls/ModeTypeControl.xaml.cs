using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ruminoid.Trimmer.Shell.Models;

namespace Ruminoid.Trimmer.Shell.Controls
{
    /// <summary>
    /// ModeTypeControl.xaml 的交互逻辑
    /// </summary>
    public partial class ModeTypeControl : UserControl, INotifyPropertyChanged
    {

        public ModeTypeControl()
        {
            InitializeComponent();
        }

        #region DataContext

        public static readonly DependencyProperty TypeListProperty = DependencyProperty.Register(
            "TypeList",
            typeof(ObservableCollection<ModeType>),
            typeof(ModeTypeControl),
            new PropertyMetadata(default(ObservableCollection<ModeType>)));

        public ObservableCollection<ModeType> TypeList
        {
            get => (ObservableCollection<ModeType>) GetValue(TypeListProperty);
            set
            {
                SetValue(TypeListProperty, value);
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty SelectedTypeProperty = DependencyProperty.Register(
            "SelectedType", typeof(ModeType), typeof(ModeTypeControl), new PropertyMetadata(default(ModeType)));

        public ModeType SelectedType
        {
            get => (ModeType) GetValue(SelectedTypeProperty);
            set
            {
                SetValue(SelectedTypeProperty, value);
                OnPropertyChanged();
            }
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
