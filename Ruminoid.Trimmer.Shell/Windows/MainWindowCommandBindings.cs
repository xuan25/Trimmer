using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ruminoid.Trimmer.Shell.Commands;
using Ruminoid.Trimmer.Shell.Dialogs;
using Ruminoid.Trimmer.Shell.Models;
using Ruminoid.Trimmer.Shell.Views;
using Squirrel;

namespace Ruminoid.Trimmer.Shell.Windows
{
    public partial class MainWindow : INotifyPropertyChanged
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

        private void Command_Save(object sender, ExecutedRoutedEventArgs e) => new SaveFileDialog().ShowDialog();

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

        #region KeyDown

        private void MainWindow_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!IsHandling) return;
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) return;
            if (
                e.Key == Key.Z ||
                e.Key == Key.OemComma ||
                e.Key == Key.X ||
                e.Key == Key.OemPeriod ||
                e.Key == Key.Space ||
                e.Key == Key.A ||
                e.Key == Key.K ||
                e.Key == Key.Left ||
                e.Key == Key.D ||
                e.Key == Key.OemSemicolon ||
                e.Key == Key.Right ||
                e.Key == Key.W ||
                e.Key == Key.O ||
                e.Key == Key.Up ||
                e.Key == Key.S ||
                e.Key == Key.L ||
                e.Key == Key.Down) e.Handled = true;
            switch (e.Key)
            {
                case Key.A:
                case Key.K:
                case Key.Left:
                    IsLeftPressed = true;
                    break;
                case Key.D:
                case Key.OemSemicolon:
                case Key.Right:
                    IsRightPressed = true;
                    break;
                case Key.W:
                case Key.O:
                case Key.Up:
                    IsUpPressed = true;
                    break;
                case Key.S:
                case Key.L:
                case Key.Down:
                    IsDownPressed = true;
                    break;
                case Key.Z:
                case Key.OemComma:
                    IsSkipPressed = true;
                    break;
                case Key.X:
                case Key.OemPeriod:
                    IsReturnPressed = true;
                    break;
            }
            TriggerKeyPress(e.Key);
        }

        private void MainWindow_OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (!IsHandling) return;
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) return;
            switch (e.Key)
            {
                case Key.A:
                case Key.K:
                case Key.Left:
                    IsLeftPressed = false;
                    break;
                case Key.D:
                case Key.OemSemicolon:
                case Key.Right:
                    IsRightPressed = false;
                    break;
                case Key.W:
                case Key.O:
                case Key.Up:
                    IsUpPressed = false;
                    break;
                case Key.S:
                case Key.L:
                case Key.Down:
                    IsDownPressed = false;
                    break;
                case Key.Z:
                case Key.OemComma:
                    IsSkipPressed = false;
                    break;
                case Key.X:
                case Key.OemPeriod:
                    IsReturnPressed = false;
                    break;
            }
        }

        private void WwControl_OnClick(object sender, RoutedEventArgs e)
        {
            if (!IsHandling) return;
            Button s = sender as Button;
            switch (s?.Tag)
            {
                case "Left":
                    TriggerKeyPress(Key.Left);
                    break;
                case "Right":
                    TriggerKeyPress(Key.Right);
                    break;
                case "Up":
                    TriggerKeyPress(Key.Up);
                    break;
                case "Down":
                    TriggerKeyPress(Key.Down);
                    break;
                case "Skip":
                    TriggerKeyPress(Key.Z);
                    break;
                case "Return":
                    TriggerKeyPress(Key.X);
                    return;
            }
        }

        private void TriggerKeyPress(Key key)
        {
            switch (key)
            {
                case Key.Space:
                    if (PlaybackView.Current.MediaLoaded) PlaybackView.Current.Playing = !PlaybackView.Current.Playing;
                    break;
                case Key.S:
                case Key.L:
                case Key.Down:
                    LyricEditorView.Current.Apply();
                    break;
                case Key.W:
                case Key.O:
                case Key.Up:
                    LyricEditorView.Current.Undo();
                    break;
                case Key.Z:
                case Key.OemComma:
                    LyricEditorView.Current.Skip();
                    break;
                case Key.X:
                case Key.OemPeriod:
                    LyricEditorView.Current.Break();
                    break;
                case Key.A:
                case Key.K:
                case Key.Left:
                    PlaybackView.Current.MediaPlayer.Time -= 1000;
                    PlaybackView.Current.Position.Time = PlaybackView.Current.MediaPlayer.Time;
                    break;
                case Key.D:
                case Key.OemSemicolon:
                case Key.Right:
                    PlaybackView.Current.MediaPlayer.Time += 1000;
                    PlaybackView.Current.Position.Time = PlaybackView.Current.MediaPlayer.Time;
                    break;
            }
        }

        #endregion

        #region DataContext

        private bool _isHandling = true;

        public bool IsHandling
        {
            get => _isHandling;
            set
            {
                _isHandling = value;
                OnPropertyChanged();
            }
        }

        private bool _isLeftPressed;

        public bool IsLeftPressed
        {
            get => _isLeftPressed;
            set
            {
                _isLeftPressed = value;
                OnPropertyChanged();
            }
        }

        private bool _isRightPressed;

        public bool IsRightPressed
        {
            get => _isRightPressed;
            set
            {
                _isRightPressed = value;
                OnPropertyChanged();
            }
        }

        private bool _isUpPressed;

        public bool IsUpPressed
        {
            get => _isUpPressed;
            set
            {
                _isUpPressed = value;
                OnPropertyChanged();
            }
        }

        private bool _isDownPressed;

        public bool IsDownPressed
        {
            get => _isDownPressed;
            set
            {
                _isDownPressed = value;
                OnPropertyChanged();
            }
        }

        private bool _isSkipPressed;

        public bool IsSkipPressed
        {
            get => _isSkipPressed;
            set
            {
                _isSkipPressed = value;
                OnPropertyChanged();
            }
        }

        private bool _isReturnPressed;

        public bool IsReturnPressed
        {
            get => _isReturnPressed;
            set
            {
                _isReturnPressed = value;
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
