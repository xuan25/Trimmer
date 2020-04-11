using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ruminoid.Trimmer.Shell.Commands;

namespace Ruminoid.Trimmer.Shell.Views
{
    public partial class PlaybackView
    {

        private void AddCommandBindings()
        {

            #region Playback

            CommandBindings.Add(new CommandBinding(
                UICommands.LoadMedia,
                Commands_LoadMedia,
                CanExecute));

            CommandBindings.Add(new CommandBinding(
                UICommands.Playback,
                Command_Playback,
                CanExecute));

            CommandBindings.Add(new CommandBinding(
                UICommands.UnloadMedia,
                Command_UnloadMedia,
                (sender, args) =>
                {
                    args.CanExecute = _mediaLoaded;
                    args.Handled = true;
                }));

            #endregion

        }

        #region Playback

        private void Commands_LoadMedia(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Command_Playback(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Command_UnloadMedia(object sender, ExecutedRoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

    }
}
