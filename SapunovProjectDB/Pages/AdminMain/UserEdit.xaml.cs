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
            if (string.IsNullOrWhiteSpace(UserLoginTextBox.Text))
            {
                ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                ValidationErrorMsg.Visibility = Visibility.Visible;
            }
            else if (string.IsNullOrWhiteSpace(UserPasswordTextBox.Text))
            {
                ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                ValidationErrorMsg.Visibility = Visibility.Visible;
            }
            else if (UserRoleComboBox.SelectedItem == null)
            {
                ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                ValidationErrorMsg.Visibility = Visibility.Visible;
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
    }
}
