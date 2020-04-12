using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Ruminoid.Trimmer.Shell.Commands;
using Ruminoid.Trimmer.Shell.Models;
using Ruminoid.Trimmer.Shell.Views;
using Squirrel;

namespace Ruminoid.Trimmer.Shell.Windows
{
    public partial class MainWindow
    {

        private bool _updating;

        private void AddCommandBindings()
        {

            #region File

            CommandBindings.Add(new CommandBinding(
                ApplicationCommands.Save,
                Command_Save,
                (sender, args) =>
                {
                    args.CanExecute = LrcModel.Current.IsModified;
                    args.Handled = true;
                }));

            CommandBindings.Add(new CommandBinding(
                UICommands.ExitApp,
                Command_ExitApp,
                CanExecute));

            #endregion

            #region View

            CommandBindings.Add(new CommandBinding(
                UICommands.ShowLyricsEditorView,
                (sender, args) => LyricEditorView.Current.DockControl?.Show(),
                CanExecute));

            CommandBindings.Add(new CommandBinding(
                UICommands.ShowPlaybackView,
                (sender, args) => PlaybackView.Current.DockControl?.Show(),
                CanExecute));

            #endregion

            #region Help

            CommandBindings.Add(new CommandBinding(
                UICommands.DoAppUpdate,
                Command_DoAppUpdate,
                (sender, args) =>
                {
                    args.CanExecute = !_updating;
                    args.Handled = true;
                }));

            #endregion

        }
        
        #region File

        private void Command_Save(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Command_ExitApp(object sender, ExecutedRoutedEventArgs e)
        {
            if (LrcModel.Current.IsModified)
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
            else Close();
        }

        #endregion

        #region Help

        private void Command_DoAppUpdate(object sender, ExecutedRoutedEventArgs e)
        {
            _updating = true;
            Task.Factory.StartNew(async () =>
            {
                using (var mgr = new UpdateManager("https://ruminoid.vbox.moe/res/trimmer/releases"))
                {
                    await mgr.UpdateApp();
                    Application.Current.Dispatcher?.Invoke(() =>
                        _updating = false);
                }
            });
        }

        #endregion

        private void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

    }
}
