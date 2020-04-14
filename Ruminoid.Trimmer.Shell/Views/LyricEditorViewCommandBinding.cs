using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Ruminoid.Trimmer.Shell.Commands;
using Ruminoid.Trimmer.Shell.Dialogs;
using Ruminoid.Trimmer.Shell.Models;

namespace Ruminoid.Trimmer.Shell.Views
{
    public partial class LyricEditorView
    {

        private void AddCommandBindings()
        {
            Application.Current.MainWindow?.CommandBindings.Add(new CommandBinding(
                UICommands.AddLyrics,
                AddLyrics_Executed,
                CanExecute));
        }

        public void AddLyrics_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            EditLineDialog.ShowAddDialog();
            if (!string.IsNullOrEmpty(EditLineDialog.Data)) LrcModel.Current.AddLyrics(EditLineDialog.Data);
        }

        private void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        #region Operations

        public void Apply()
        {

        }

        public void Undo()
        {

        }

        #endregion

    }
}
