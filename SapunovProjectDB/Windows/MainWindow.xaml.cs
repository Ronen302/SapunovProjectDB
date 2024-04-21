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
            if (Properties.Settings.Default.ColorMode == "Light")
            {
                changeThemeIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.MoonWaningCrescent;
            }
            else
            {
                changeThemeIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.WhiteBalanceSunny;
                changeThemeIcon.Width = 14;
                changeThemeIcon.Height = 14;
            }
            if (Properties.Settings.Default.IsMaximized == true)
            {
                maximizeIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.WindowRestore;
                MainBorder.CornerRadius = new CornerRadius(0);
                topBorder.CornerRadius = new CornerRadius(0);
                CloseBtn.Visibility = Visibility.Collapsed;
                CloseBtnMaximized.Visibility = Visibility.Visible;
                WindowState = WindowState.Maximized;
                WindowState = WindowState.Maximized;
            }
            else
                WindowState = WindowState.Normal;
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                maximizeIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.WindowRestore;
                MainBorder.CornerRadius = new CornerRadius(0);
                topBorder.CornerRadius = new CornerRadius(0);
                CloseBtn.Visibility = Visibility.Collapsed;
                CloseBtnMaximized.Visibility = Visibility.Visible;
                WindowState = WindowState.Maximized;
                Properties.Settings.Default.IsMaximized = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                maximizeIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.WindowMaximize;
                MainBorder.CornerRadius = new CornerRadius(10);
                topBorder.CornerRadius = new CornerRadius(10, 10, 0, 0);
                CloseBtnMaximized.Visibility = Visibility.Collapsed;
                CloseBtn.Visibility = Visibility.Visible;
                WindowState = WindowState.Normal;
                Properties.Settings.Default.IsMaximized = false;
                Properties.Settings.Default.Save();
            }
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
                changeThemeIcon.Width = 14;
                changeThemeIcon.Height = 14;
                changeThemeIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.WhiteBalanceSunny;
            }
            else
            {
                Properties.Settings.Default.ColorMode = "Light";
                Properties.Settings.Default.Save();
                changeThemeIcon.Width = 12;
                changeThemeIcon.Height = 12;
                changeThemeIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.MoonWaningCrescent;
            }
        }

        private void CloseBtnMaximized_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void topBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (WindowState == WindowState.Normal)
                {
                    maximizeIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.WindowRestore;
                    MainBorder.CornerRadius = new CornerRadius(0);
                    topBorder.CornerRadius = new CornerRadius(0);
                    CloseBtn.Visibility = Visibility.Collapsed;
                    CloseBtnMaximized.Visibility = Visibility.Visible;
                    WindowState = WindowState.Maximized;
                    Properties.Settings.Default.IsMaximized = true;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    maximizeIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.WindowMaximize;
                    MainBorder.CornerRadius = new CornerRadius(10);
                    topBorder.CornerRadius = new CornerRadius(10, 10, 0, 0);
                    CloseBtnMaximized.Visibility = Visibility.Collapsed;
                    CloseBtn.Visibility = Visibility.Visible;
                    WindowState = WindowState.Normal;
                    Properties.Settings.Default.IsMaximized = false;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void topBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
