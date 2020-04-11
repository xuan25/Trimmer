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

        public static RoutedUICommand ShowLyricsEditorView { get; } = new RoutedUICommand(
            "编辑(_E)",
            "ShowLyricsEditorView",
            typeof(UICommands),
            new InputGestureCollection(new List<InputGesture>
            {
                new KeyGesture(Key.F3, ModifierKeys.None, "F3")
            }));

        public static RoutedUICommand ShowPlaybackView { get; } = new RoutedUICommand(
            "回放(_P)",
            "ShowPlaybackView",
            typeof(UICommands),
            new InputGestureCollection(new List<InputGesture>
            {
                new KeyGesture(Key.F4, ModifierKeys.None, "F4")
            }));

    }
}
