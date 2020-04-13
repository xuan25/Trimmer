using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Ruminoid.Trimmer.Shell.Dialogs
{
    /// <summary>
    /// EditLineDialog.xaml 的交互逻辑
    /// </summary>
    public partial class EditLineDialog : INotifyPropertyChanged
    {

        public EditLineDialog()
        {
            InitializeComponent();
        }

        #region ShowHelper

        public static void ShowAddDialog()
        {
            CheckDialog();
            Current.IsAddMode = true;
            Current.ShowDialog();
        }

        public static void ShowEditDialog()
        {
            CheckDialog();
            Current.IsAddMode = false;
            Current.ShowDialog();
        }

        private static void CheckDialog()
        {
            if (Current is null) Current = new EditLineDialog();
        }

        #endregion

        #region Current

        public static EditLineDialog Current { get; set; } = new EditLineDialog();

        public static string Data { get; set; };

        #endregion

        #region DataContext

        private bool _isAddMode;

        public bool IsAddMode
        {
            get => _isAddMode;
            set 
            {
                _isAddMode = value;
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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Button s = sender as Button;
            if (s?.Tag != null && s.Tag.ToString() == "Apply") Data = InputBox.Text;
            Close();
        }
    }
}
