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
using Microsoft.WindowsAPICodePack.Dialogs;
using Ruminoid.Trimmer.Shell.Models;

namespace Ruminoid.Trimmer.Shell.Dialogs
{
    /// <summary>
    /// SaveFileDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SaveFileDialog : INotifyPropertyChanged
    {

        public SaveFileDialog()
        {
            InitializeComponent();
        }

        #region DataContext

        private string _filePath = "";

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                OnPropertyChanged();
            }
        }

        private ModeType _mode = ModeType.KType[0];

        public ModeType Mode
        {
            get => _mode;
            set
            {
                _mode = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Event Handler

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Button s = sender as Button;
            if (s?.Tag != null && s.Tag.ToString() == "Save")
            {
                CommonSaveFileDialog saveFileDialog = new CommonSaveFileDialog
                {
                    Title = "保存",
                    DefaultDirectory = Environment.CurrentDirectory,
                    EnsurePathExists = true,
                    Filters = { new CommonFileDialogFilter("ASS 字幕文件", ".ass") },
                    EnsureFileExists = true
                };
                if (saveFileDialog.ShowDialog() == CommonFileDialogResult.Ok) FilePath = saveFileDialog.FileName;
                Activate();
                return;
            }
            if (s?.Tag != null && s.Tag.ToString() == "Apply")
            {
                if (string.IsNullOrEmpty(FilePath))
                {
                    MessageBox.Show(
                        "文件路径为空。请检查文件路径。",
                        "错误",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error,
                        MessageBoxResult.OK);
                    return;
                }

                try
                {
                    new LrcExporter(FilePath, Mode, Encoding.UTF8).Export();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(
                        "保存时发生错误。请按照如下顺序进行检查。\n1.检查时间戳是否完整；\n2.检查最后一行是否含有折行符。\n详细信息：\n" +
                        exception.Message,
                        "错误",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error,
                        MessageBoxResult.OK);
                    return;
                }
            }
            Close();
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
