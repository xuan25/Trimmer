using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Ruminoid.Trimmer.Shell.Commands;
using Ruminoid.Trimmer.Shell.Helpers;
using Ruminoid.Trimmer.Shell.Views;
using YDock.Enum;

namespace Ruminoid.Trimmer.Shell.Windows
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += OnLoaded;

            Closing += OnClosing;

            Closed += (sender, args) => Application.Current.Shutdown(0);

            #region Command Bindings

            CommandBindings.Add(new CommandBinding(
                UICommands.ExitApp,
                (o, args) => Close(),
                (o, args) => args.CanExecute = true));

            #endregion

            #region Document Register

            DockManager.RegisterDocument(TextEditorView.Current);

            #endregion
        }

        private static readonly string SettingFileName = Path.Combine(ConfigHelper.UserDataFolder, "layout.xml");

        private void OnClosing(object sender, CancelEventArgs e)
        {
            Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "settings"));
            DockManager.SaveCurrentLayout("MainWindow");
            var doc = new XDocument();
            var rootNode = new XElement("Layouts");
            foreach (var layout in DockManager.Layouts.Values)
                layout.Save(rootNode);
            doc.Add(rootNode);
            doc.Save(SettingFileName);
            DockManager.Dispose();

            ConfigHelper.SaveConfig();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            HwndSource.FromHwnd(hwnd).AddHook(WndProc);
            wndList = new List<FrameworkElement>() { Wnd1, Wnd2, Wnd3 };

            if (File.Exists(SettingFileName))
            {
                XDocument layout = XDocument.Parse(File.ReadAllText(SettingFileName));
                if (layout.Root != null)
                    foreach (XElement item in layout.Root.Elements())
                    {
                        string name = item.Attribute("Name")?.Value;
                        if (string.IsNullOrEmpty(name)) continue;
                        if (DockManager.Layouts.ContainsKey(name))
                            DockManager.Layouts[name].Load(item);
                        else DockManager.Layouts[name] = new YDock.LayoutSetting.LayoutSetting(name, item);
                    }
                DockManager.ApplyLayout("MainWindow");
            }
            else
            {
                TextEditorView.Current.DockControl.Show();
            }
        }

        #region CaptionBar Hook

        private List<FrameworkElement> wndList;

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_NCHITTEST)
            {
                if (wndList is null) return IntPtr.Zero;
                Point p = new Point();
                int pInt = lParam.ToInt32();
                p.X = (pInt << 16) >> 16;
                p.Y = pInt >> 16;
                if (WndCaption.PointFromScreen(p).Y > WndCaption.ActualHeight) return IntPtr.Zero;
                foreach (FrameworkElement element in wndList)
                {
                    Point rel = element.PointFromScreen(p);
                    if (rel.X >= 0 && rel.X <= element.ActualWidth && rel.Y >= 0 && rel.Y <= element.ActualHeight)
                    {
                        return IntPtr.Zero;
                    }
                }
                handled = true;
                return new IntPtr(2);
            }

            return IntPtr.Zero;
        }

        private const int WM_NCHITTEST = 0x0084;

        #endregion

        #region Utilities

        private void ToolBar_RemoveGrid(object sender, RoutedEventArgs e)
        {

            ToolBar toolBar = sender as ToolBar;
            if (toolBar?.Template.FindName("OverflowGrid", toolBar) is FrameworkElement overflowGrid)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }
            if (toolBar?.Template.FindName("MainPanelBorder", toolBar) is FrameworkElement mainPanelBorder)
            {
                mainPanelBorder.Margin = new Thickness(0);
            }

        }

        #endregion
    }
}
