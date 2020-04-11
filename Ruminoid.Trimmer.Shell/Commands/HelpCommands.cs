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

        public static RoutedUICommand DoAppUpdate { get; } = new RoutedUICommand(
            "检查更新(_C)",
            "DoAppUpdate",
            typeof(UICommands),
            new InputGestureCollection(new List<InputGesture>()));

    }
}
