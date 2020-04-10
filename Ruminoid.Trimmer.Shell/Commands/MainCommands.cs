using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ruminoid.Trimmer.Shell.Commands
{
    public static partial class UICommands
    {

        public static RoutedUICommand ExitApp { get; } = new RoutedUICommand(
            "退出",
            "Exit",
            typeof(UICommands),
            new InputGestureCollection(new List<InputGesture>()
            {
                new KeyGesture(Key.F4, ModifierKeys.Alt, "Alt+F4")
            }));

    }
}
