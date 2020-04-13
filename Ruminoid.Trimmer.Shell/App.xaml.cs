using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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
            DispatcherUnhandledException += (sender, args) =>
            {
                args.Handled = true;
                if (args.Exception.Source.Contains("VLC") || args.Exception.Message.Contains("VideoView")) return;
                MessageBox.Show(
                    args.Exception.Message,
                    "灾难性故障",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error,
                    MessageBoxResult.OK);
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                if (((Exception) args.ExceptionObject).Source.Contains("VLC") ||
                    ((Exception) args.ExceptionObject).Message.Contains("VideoView")) return;
                MessageBox.Show(
                    ((Exception) args.ExceptionObject)?.Message ?? "Exception",
                    "灾难性故障",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error,
                    MessageBoxResult.OK);
            };

            Shell.Properties.Resources.Culture = CultureInfo.CurrentUICulture;

            if (MainWindow is null) MainWindow = new MainWindow();
            MainWindow.Show();

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
