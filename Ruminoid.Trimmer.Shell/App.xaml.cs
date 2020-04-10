using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace Ruminoid.Trimmer.Shell
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
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
        }
    }
}
