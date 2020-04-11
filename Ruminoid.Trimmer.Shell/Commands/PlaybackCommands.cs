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

        public static RoutedUICommand LoadMedia { get; } = new RoutedUICommand(
            "加载媒体(_L)",
            "LoadMedia",
            typeof(UICommands),
            new InputGestureCollection(new List<InputGesture>
            {
                new KeyGesture(Key.L, ModifierKeys.Control, "Ctrl+L"),
                new KeyGesture(Key.F7, ModifierKeys.None, "F7")
            }));

        public static RoutedUICommand Playback { get; } = new RoutedUICommand(
            "播放/暂停(_P)",
            "Playback",
            typeof(UICommands),
            new InputGestureCollection(new List<InputGesture>
            {
                new KeyGesture(Key.Space, ModifierKeys.None, "空格")
            }));

        public static RoutedUICommand UnloadMedia { get; } = new RoutedUICommand(
            "卸载媒体(_U)",
            "UnloadMedia",
            typeof(UICommands),
            new InputGestureCollection(new List<InputGesture>
            {
                new KeyGesture(Key.U, ModifierKeys.Control, "Ctrl+U")
            }));

    }
}
