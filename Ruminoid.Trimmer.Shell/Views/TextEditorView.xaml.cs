using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using YDock.Interface;

namespace Ruminoid.Trimmer.Shell.Views
{
    /// <summary>
    /// TextEditorView.xaml 的交互逻辑
    /// </summary>
    public partial class TextEditorView : UserControl, IDockSource
    {
        public TextEditorView()
        {

            using (var stream = new MemoryStream(Shell.Properties.Resources.LrcMode))
            {
                using (var reader = new XmlTextReader(stream))
                {
                    HighlightingManager.Instance.RegisterHighlighting("Lrc", new string[0],
                        HighlightingLoader.Load(reader,
                            HighlightingManager.Instance));
                }
            }

            InitializeComponent();

        }

        #region Current

        public static TextEditorView Current { get; } = new TextEditorView();

        #endregion

        #region DockSource

        public IDockControl DockControl { get; set; }
        public string Header => "编辑";
        public ImageSource Icon => null;

        #endregion
    }
}
