using SapunovProjectDB.Pages;
using SapunovProjectDB.Classes;
using System.Windows;
using System.Windows.Input;

namespace SapunovProjectDB.Windows
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowFrame.Navigate(new Authorization());
            var primaryMonitorArea = SystemParameters.WorkArea;
            this.Left = primaryMonitorArea.Right - this.Width;
            this.Top = primaryMonitorArea.Bottom - this.Height;

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (WindowState == WindowState.Normal)
                    WindowState = WindowState.Maximized;
                else
                    WindowState = WindowState.Normal;
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ChangeThemeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.ColorMode == "Light")
            {
                Properties.Settings.Default.ColorMode = "Dark";
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.ColorMode = "Light";
                Properties.Settings.Default.Save();
            }
        }
    }
}
