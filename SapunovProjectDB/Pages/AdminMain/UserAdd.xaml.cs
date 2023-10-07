using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SapunovProjectDB.Pages.AdminMain
{
    public partial class UserAdd : Page
    {
        public UserAdd()
        {
            InitializeComponent();
            UserRoleComboBox.ItemsSource = DBEntities.GetContext().Role.ToList();
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
            else if (DBEntities.GetContext().User.FirstOrDefault(u => u.LoginUser == UserLoginTextBox.Text) != null)
            {
                ValidationErrorMsg.Text = "Такой логин уже существует";
                ValidationErrorMsg.Visibility = Visibility.Visible;
            }
            else
            {
                try
                {
                    var newUser = new User()
                    {
                        LoginUser = UserLoginTextBox.Text,
                        PasswordUser = UserPasswordTextBox.Text,
                        IdRole = Int32.Parse(UserRoleComboBox.SelectedValue.ToString())
                    };
                    DBEntities.GetContext().User.Add(newUser);
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
