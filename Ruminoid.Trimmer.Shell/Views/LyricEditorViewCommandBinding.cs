using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
            string data = EditLineDialog.GetData();
            if (string.IsNullOrEmpty(data)) return;
            LrcModel.Current.AddLyrics(data);
        }

        private void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }

        #region Operations

        public void Apply() => LrcModel.Current.Apply(new Position(PlaybackView.Current.MediaPlayer.Time));

        public void Undo() => LrcModel.Current.Undo();

        private void EditLineButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Button s = sender as Button;
            if (!(s?.DataContext is LrcLine line)) return;
            EditLineDialog.ShowEditDialog((line.Origin ?? "").Replace("\r", "").Replace("\n", ""));
            string data = EditLineDialog.GetData();
            if (string.IsNullOrEmpty(data)) return;
            LrcModel.Current.ResetLineData(line, data);
        }

        private void DeleteLineButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "删除这行？",
                "删除",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning,
                MessageBoxResult.No);
            if (result != MessageBoxResult.Yes) return;
            if (!(sender is Button s)) return;
            LrcLine line = s.DataContext as LrcLine;
            LrcModel.Current.RemoveLine(line);
        }

        #endregion

    }
}
