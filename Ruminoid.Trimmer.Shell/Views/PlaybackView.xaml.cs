using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YDock.Interface;

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
        }

        #region DockSource

        public IDockControl DockControl { get; set; }
        public string Header => "回放";
        public ImageSource Icon => null;

        #endregion

    }
}
