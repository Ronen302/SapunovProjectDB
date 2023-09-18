using SapunovProjectDB.Pages.AdminMain;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SapunovProjectDB.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            MainPageFrame.Navigate(new UserList());
            if (Properties.Settings.Default.ColorMode == "Dark")
                changeThemeCeckBox.IsChecked = true;
            else
                changeThemeCeckBox.IsChecked = false;
        }

        private void UserListBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StaffListBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EquipmentListBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClientListBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Authorization());
        }
        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void MaximizeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Normal)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AccountToggleButton_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void changeThemeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (changeThemeCeckBox.IsChecked == false)
            {
                Properties.Settings.Default.ColorMode = "Dark";
                Properties.Settings.Default.Save();
                changeThemeCeckBox.IsChecked = true;
            }
            else if (changeThemeCeckBox.IsChecked == true)
            {
                Properties.Settings.Default.ColorMode = "Light";
                Properties.Settings.Default.Save();
                changeThemeCeckBox.IsChecked = false;
            }
        }

        private void changeThemeCeckBox_Click(object sender, RoutedEventArgs e)
        {
            if (changeThemeCeckBox.IsChecked == false)
            {
                Properties.Settings.Default.ColorMode = "Dark";
                Properties.Settings.Default.Save();
                changeThemeCeckBox.IsChecked = true;
            }
            else if (changeThemeCeckBox.IsChecked == true)
            {
                Properties.Settings.Default.ColorMode = "Light";
                Properties.Settings.Default.Save();
                changeThemeCeckBox.IsChecked = false;
            }
        }
    }
}
