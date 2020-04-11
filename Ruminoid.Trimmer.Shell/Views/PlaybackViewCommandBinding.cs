using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LibVLCSharp.Shared;
using Microsoft.WindowsAPICodePack.Dialogs;
using Ruminoid.Trimmer.Shell.Commands;

namespace Ruminoid.Trimmer.Shell.Views
{
    public partial class PlaybackView
    {

        private void AddCommandBindings()
        {

            #region Playback

            Application.Current.MainWindow?.CommandBindings.Add(new CommandBinding(
                UICommands.LoadMedia,
                Commands_LoadMedia,
                CanExecute));

            Application.Current.MainWindow?.CommandBindings.Add(new CommandBinding(
                UICommands.Playback,
                Command_Playback,
                (sender, args) =>
                {
                    args.CanExecute = MediaLoaded;
                    args.Handled = true;
                }));

            Application.Current.MainWindow?.CommandBindings.Add(new CommandBinding(
                UICommands.UnloadMedia,
                Command_UnloadMedia,
                (sender, args) =>
                {
                    args.CanExecute = MediaLoaded;
                    args.Handled = true;
                }));

            #endregion

        }

        #region Playback

        private void Commands_LoadMedia(object sender, ExecutedRoutedEventArgs e)
        {
            Command_UnloadMedia(null, null);
            CommonOpenFileDialog fileDialog = new CommonOpenFileDialog
            {
                Title = "打开媒体文件",
                DefaultDirectory = Environment.CurrentDirectory,
                IsFolderPicker = false,
                AllowNonFileSystemItems = true,
                EnsurePathExists = true,
                Multiselect = false,
                //Filters = { new CommonFileDialogFilter("波形音频", ".wav") },
                EnsureFileExists = true
            };
            if (fileDialog.ShowDialog() != CommonFileDialogResult.Ok)
                return;
            MediaLoaded = true;
            Playing = true;
            MediaPlayer.Play(new Media(_libVLC, fileDialog.FileName));
        }

        private void Command_Playback(object sender, ExecutedRoutedEventArgs e)
        {
            if (MediaLoaded) _playing = !_playing;
        }

        private void Command_UnloadMedia(object sender, ExecutedRoutedEventArgs e)
        {
            MediaPlayer.Stop();
            MediaLoaded = false;
            Position.Time = 0;
        }

        #endregion

        private void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

    }
}
