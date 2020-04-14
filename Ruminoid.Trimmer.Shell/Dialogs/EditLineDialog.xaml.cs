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

            Loaded += OnLoaded;

        }

        #region OnLoaded

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            InputBox.Focus();
        }

        #endregion

        #region ShowHelper

        public static void ShowAddDialog()
        {
            CheckDialog();
            Current.IsAddMode = true;
            Current.ShowDialog();
        }

        public static void ShowEditDialog(string origin = "")
        {
            CheckDialog();
            Current.IsAddMode = false;
            Current.InputBox.Text = origin;
            Current.ShowDialog();
        }

        private static void CheckDialog()
        {
            Current = new EditLineDialog();
        }

        #endregion

        #region Current

        public static EditLineDialog Current { get; set; } = new EditLineDialog();

        private static string _data;

        public static string GetData()
        {
            string data = _data;
            _data = null;
            return data;
        }

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
            if (s?.Tag != null && s.Tag.ToString() == "Apply") _data = InputBox.Text;
            Close();
        }

        private void EditLineDialog_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                e.Handled = true;
                Close();
            }
            if (e.Key != Key.Enter) return;
            if (!IsAddMode)
            {
                _data = InputBox.Text;
                e.Handled = true;
                Close();
                return;
            }
            if ((Keyboard.Modifiers & ModifierKeys.Control) != ModifierKeys.Control) return;
            _data = InputBox.Text;
            e.Handled = true;
            Close();
        }

    }
}
