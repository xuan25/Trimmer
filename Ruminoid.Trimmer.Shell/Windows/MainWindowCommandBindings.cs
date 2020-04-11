using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Ruminoid.Trimmer.Shell.Commands;
using Ruminoid.Trimmer.Shell.Models;

namespace Ruminoid.Trimmer.Shell.Windows
{
    public partial class MainWindow
    {

        private void AddCommandBindings()
        {

            #region File

            CommandBindings.Add(new CommandBinding(
                ApplicationCommands.Save,
                Command_Save,
                (sender, args) =>
                {
                    args.CanExecute = LrcModel.Current.Modified;
                    args.Handled = true;
                }));

            CommandBindings.Add(new CommandBinding(
                UICommands.ExitApp,
                Command_ExitApp,
                CanExecute));

            #endregion

        }

        private void Command_Save(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        private void Command_ExitApp(object sender, ExecutedRoutedEventArgs e)
        {
            if (LrcModel.Current.Modified)
            {
                MessageBoxResult result = MessageBox.Show(
                    "存在未保存的修改。是否仍要退出？",
                    "更改未保存",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning,
                    MessageBoxResult.Yes);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        Command_Save(null, null);
                        Close();
                        break;
                    case MessageBoxResult.No:
                        Close();
                        break;
                }
            }
        }

        private void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }
    }
}
