using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SapunovProjectDB.Windows.AddEditWindows
{
    public partial class UserAddEdit : Window
    {
        User _currentUser = new User();
        User _newUser = new User();
        public UserAddEdit(User selectedUser)
        {
            InitializeComponent();
            User user = DBEntities.GetContext().User
                .FirstOrDefault(u => u.LoginUser == Properties.Settings.Default.CurrentUser);
            if (user.IdRole == 2)
                UserLoginPanel.Visibility = Visibility.Collapsed;
            if (selectedUser != null)
            {
                _currentUser = selectedUser;
                CancelChangesButton.Visibility = Visibility.Visible;
                UserLoginTextBox.Text = selectedUser.LoginUser;
                UserPasswordTextBox.Text = selectedUser.PasswordUser;
                UserRoleComboBox.SelectedItem = selectedUser.Role;
            }
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
            string upperCaseLetters = "QWERTYUIOPASDFGHJKLZXCVBNMЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ",
                lowerCaseLetters = "qwertyuiopasdfghjklzxcvbnmйцукенгшщзхъфывапролджэячсмитьбю",
                numbers = "1234567890",
                enteredPassword = UserPasswordTextBox.Text;
            if (_currentUser == null)
            {
                if (DBEntities.GetContext().User.FirstOrDefault(u => u.LoginUser == UserLoginTextBox.Text) != null)
                {
                    ValidationErrorMsg.Text = "Такой логин уже существует";
                    ValidationErrorMsg.Visibility = Visibility.Visible;
                }
                else if (upperCaseLetters.IndexOfAny(enteredPassword.ToCharArray()) == -1)
                {
                    ValidationErrorMsg.Text = "Некорректный пароль";
                    ValidationErrorMsg.Visibility = Visibility.Visible;
                }
                else if (lowerCaseLetters.IndexOfAny(enteredPassword.ToCharArray()) == -1)
                {
                    ValidationErrorMsg.Text = "Некорректный пароль";
                    ValidationErrorMsg.Visibility = Visibility.Visible;
                }
                else if (numbers.IndexOfAny(enteredPassword.ToCharArray()) == -1)
                {
                    ValidationErrorMsg.Text = "Некорректный пароль";
                    ValidationErrorMsg.Visibility = Visibility.Visible;
                }
                else
                {
                    try
                    {
                        _newUser.LoginUser = UserLoginTextBox.Text;
                        _newUser.PasswordUser = UserPasswordTextBox.Text;
                        _newUser.IdRole = Int32.Parse(UserRoleComboBox.SelectedValue.ToString());
                        DBEntities.GetContext().User.Add(_newUser);
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
                try
                {
                    if (_currentUser == DBEntities.GetContext().User
                        .FirstOrDefault(u => u.LoginUser == Properties.Settings.Default.CurrentUser))
                    {
                        _currentUser = DBEntities.GetContext().User.FirstOrDefault(u => u.IdUser == _currentUser.IdUser);
                        _currentUser.LoginUser = UserLoginTextBox.Text;
                        _currentUser.PasswordUser = UserPasswordTextBox.Text;
                        _currentUser.IdRole = Int32.Parse(UserRoleComboBox.SelectedValue.ToString());
                        DBEntities.GetContext().SaveChanges();
                        Properties.Settings.Default.CurrentUser = UserLoginTextBox.Text;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        _currentUser = DBEntities.GetContext().User.FirstOrDefault(u => u.IdUser == _currentUser.IdUser);
                        _currentUser.LoginUser = UserLoginTextBox.Text;
                        _currentUser.PasswordUser = UserPasswordTextBox.Text;
                        _currentUser.IdRole = Int32.Parse(UserRoleComboBox.SelectedValue.ToString());
                        DBEntities.GetContext().SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Error.ErrorMB(ex);
                }
                DialogResult = true;
            }
        }

        private void UserLoginTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserLoginTextBox.Text) |
               string.IsNullOrWhiteSpace(UserPasswordTextBox.Text) |
               UserRoleComboBox.SelectedValue == null)
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void UserPasswordTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserLoginTextBox.Text) |
               string.IsNullOrWhiteSpace(UserPasswordTextBox.Text) |
               UserRoleComboBox.SelectedValue == null)
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void UserRoleComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserLoginTextBox.Text) |
               string.IsNullOrWhiteSpace(UserPasswordTextBox.Text) |
               UserRoleComboBox.SelectedValue == null)
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void CancelChangesButton_Click(object sender, RoutedEventArgs e)
        {
            UserLoginTextBox.Text = _currentUser.LoginUser;
            UserPasswordTextBox.Text = _currentUser.PasswordUser;
            UserRoleComboBox.SelectedItem = _currentUser.Role;
        }
    }
}
