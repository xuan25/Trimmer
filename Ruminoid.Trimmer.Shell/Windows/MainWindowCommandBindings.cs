using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Ruminoid.Trimmer.Shell.Commands;

namespace Ruminoid.Trimmer.Shell.Windows
{
    public partial class MainWindow
    {

        private void AddCommandBindings()
        {

            #region File

            CommandBindings.Add(new CommandBinding(
                UICommands.ExitApp,
                (o, args) => Close(),
                CanExecute));

            #endregion

        }

        private void CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            e.Handled = true;
        }
    }
}
