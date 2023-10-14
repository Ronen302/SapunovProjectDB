using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SapunovProjectDB.Windows.AdminMain
{
    public partial class UserAddEdit : Window
    {
        private readonly User _currentUser = new User();
        public UserAddEdit(User selectedUser)
        {
            InitializeComponent();
            if (selectedUser != null)
                _currentUser = selectedUser;
            else
                _currentUser = null;
            DataContext = _currentUser;
            UserRoleComboBox.ItemsSource = DBEntities.GetContext().Role.ToList();
        }

        private void MainBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void userSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser == null)
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
                    }
                    catch (Exception ex)
                    {
                        Error.ErrorMB(ex);
                    }
                    DialogResult = true;
                }
            }
            else
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
                        _currentUser.LoginUser = UserLoginTextBox.Text;
                        _currentUser.PasswordUser = UserPasswordTextBox.Text;
                        _currentUser.IdRole = Int32.Parse(UserRoleComboBox.SelectedValue.ToString());
                        DBEntities.GetContext().SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Error.ErrorMB(ex);
                    }
                    DialogResult = true;
                }
            }
        }
    }
}
