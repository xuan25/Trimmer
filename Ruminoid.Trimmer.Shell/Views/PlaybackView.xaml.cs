using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using LibVLCSharp.Shared;
using YDock.Interface;
using MediaPlayer = LibVLCSharp.Shared.MediaPlayer;

namespace Ruminoid.Trimmer.Shell.Views
{
    /// <summary>
    /// PlaybackView.xaml 的交互逻辑
    /// </summary>
    public partial class PlaybackView : UserControl, IDockSource
    {

        public PlaybackView()
        {

            InitializeComponent();

            Core.Initialize();
            _libVLC = new LibVLC();
            MediaPlayer = new MediaPlayer(_libVLC);
            VideoView.MediaPlayer = MediaPlayer;
            MediaPlayer.TimeChanged += (o, args) => Position.Time = args.Time;
            MediaPlayer.LengthChanged += (o, args) => Position.Total = args.Length;

            AddCommandBindings();

        }

        #region VLC

        private LibVLC _libVLC;
        public MediaPlayer MediaPlayer;

        #endregion

        #region Current

        public static PlaybackView Current { get; } = new PlaybackView();

        #endregion

        #region DockSource

        public IDockControl DockControl { get; set; }
        public string Header => "回放";
        public ImageSource Icon => null;

        #endregion

    }
}
