using SapunovProjectDB.Data;
using System.Linq;
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
            User user = DBEntities.GetContext().User
                .FirstOrDefault(u => u.LoginUser == Properties.Settings.Default.CurrentUser);
            switch (user.IdRole)
            {
                case 1:
                    userRoleTextBlock.Text = "Админитсратор";
                    MainPageFrame.Navigate(new UserList());
                    break;
                case 2:
                    userRoleTextBlock.Text = "Менеджер";
                    MainPageFrame.Navigate(new OrderList());
                    break;
                case 3:
                    userRoleTextBlock.Text = "Сотрудник";
                    UserListBtn.Visibility = Visibility.Collapsed;
                    StaffListBtn.Visibility = Visibility.Collapsed;
                    ClientListBtn.Visibility = Visibility.Collapsed;
                    MainPageFrame.Navigate(new OrderList());
                    break;
                case 4:
                    userRoleTextBlock.Text = "Клиент";
                    UserListBtn.Visibility = Visibility.Collapsed;
                    StaffListBtn.Visibility = Visibility.Collapsed;
                    ClientListBtn.Visibility = Visibility.Collapsed;
                    OrderListBtn.Visibility = Visibility.Collapsed;
                    MainPageFrame.Navigate(new ServiceList());
                    break;
            }
        }

        private void UserListBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Navigate(new UserList());
        }

        private void StaffListBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Navigate(new StaffList());
        }

        private void EquipmentListBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Navigate(new ServiceList());
        }

        private void ClientListBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Navigate(new ClientList());
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Authorization());
        }

        private void OrderListBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Navigate(new OrderList());
        }

        private void MyOrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Navigate(new MyOrdersList());
        }

        private void AccountSettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Navigate(new AccountSettings());
        }
    }
}
