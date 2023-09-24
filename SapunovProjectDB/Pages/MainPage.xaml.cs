using SapunovProjectDB.Pages.AdminMain;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SapunovProjectDB.Pages
{
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
            MainPageFrame.Navigate(new TypeOfServiceList());
        }

        private void ClientListBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Authorization());
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
