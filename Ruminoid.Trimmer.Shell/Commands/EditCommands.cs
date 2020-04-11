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

        public static RoutedUICommand AddLyrics { get; } = new RoutedUICommand(
            "添加歌词...",
            "AddLyrics",
            typeof(UICommands),
            new InputGestureCollection(new List<InputGesture>
            {
                new KeyGesture(Key.T, ModifierKeys.Control, "Ctrl+T"),
                new KeyGesture(Key.F5, ModifierKeys.None, "F5")
            }));

        public static RoutedUICommand EditCurrentLine { get; } = new RoutedUICommand(
            "编辑当前行...",
            "EditCurrentLine",
            typeof(UICommands),
            new InputGestureCollection(new List<InputGesture>
            {
                new KeyGesture(Key.T, ModifierKeys.Control, "Ctrl+T"),
                new KeyGesture(Key.Space, ModifierKeys.Control, "Ctrl+空格"),
                new KeyGesture(Key.F6, ModifierKeys.None, "F6")
            }));

    }
}
