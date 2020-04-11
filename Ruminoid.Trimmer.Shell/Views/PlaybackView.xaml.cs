using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

            Loaded += OnLoaded;

        }

        #region VLC

        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;

        #endregion

        #region Loaded

        private void OnLoaded(object sender, RoutedEventArgs e)
        {

            Core.Initialize();
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
            VideoView.MediaPlayer = _mediaPlayer;

        }

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
