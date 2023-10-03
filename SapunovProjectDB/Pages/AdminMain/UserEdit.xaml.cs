using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SapunovProjectDB.Pages.AdminMain
{
    public partial class UserEdit : Page
    {
        User user = new User();
        public UserEdit(User user)
        {
            InitializeComponent();
            DataContext = user;
            UserRoleComboBox.ItemsSource = DBEntities.GetContext().Role.ToList();
            this.user.IdUser = user.IdUser;
        }

        private void userSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserLoginTextBox.Text) &&
                string.IsNullOrWhiteSpace(UserPasswordTextBox.Text) &&
                UserRoleComboBox.SelectedItem == null)
            {
                EmptyLoginErrorAddUser.Visibility = Visibility.Visible;
                EmptyPasswordErrorAddUser.Visibility = Visibility.Visible;
                EmptyRoleErrorAddUser.Visibility = Visibility.Visible;
            }
            else if (string.IsNullOrWhiteSpace(UserLoginTextBox.Text))
            {
                EmptyLoginErrorAddUser.Visibility = Visibility.Visible;
            }
            else if (string.IsNullOrWhiteSpace(UserPasswordTextBox.Text))
            {
                EmptyPasswordErrorAddUser.Visibility = Visibility.Visible;
            }
            else if (UserRoleComboBox.SelectedItem == null)
            {
                EmptyRoleErrorAddUser.Visibility = Visibility.Visible;
            }
            else
            {
                try
                {
                    user = DBEntities.GetContext().User.FirstOrDefault(u => u.IdUser == user.IdUser);
                    user.LoginUser = UserLoginTextBox.Text;
                    user.PasswordUser = UserPasswordTextBox.Text;
                    user.IdRole = Int32.Parse(UserRoleComboBox.SelectedValue.ToString());
                    DBEntities.GetContext().SaveChanges();
                    NavigationService.Navigate(new UserList());
                }
                catch (Exception ex)
                {
                    Error.ErrorMB(ex);
                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Content = null;
        }

        private void UserLoginTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EmptyLoginErrorAddUser.Visibility = Visibility.Collapsed;
        }

        private void UserPasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EmptyPasswordErrorAddUser.Visibility = Visibility.Collapsed;
        }

        private void UserRoleComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EmptyRoleErrorAddUser.Visibility = Visibility.Collapsed;
        }
    }
}
