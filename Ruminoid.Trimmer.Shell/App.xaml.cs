using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using MetroRadiance.UI;
using Ruminoid.Trimmer.Shell.Windows;

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

            DispatcherUnhandledException += (sender, args) =>
            {
                args.Handled = true;
                MessageBox.Show(
                    args.Exception.Message,
                    "灾难性故障",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error,
                    MessageBoxResult.OK);
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                MessageBox.Show(
                    (args.ExceptionObject as Exception)?.Message ?? "Exception",
                    "灾难性故障",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error,
                    MessageBoxResult.OK);
            };

            Shell.Properties.Resources.Culture = CultureInfo.CurrentUICulture;

            StartWindow.Current.Show();

            Current.Dispatcher?.Invoke(() => ThemeService.Current.ChangeTheme(Theme.Dark));
            Current.Dispatcher?.Invoke(() => ThemeService.Current.ChangeAccent(Accent.Blue));

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ThemeService.Current.Register(this, Theme.Windows, Accent.Windows);
        }
    }
}
