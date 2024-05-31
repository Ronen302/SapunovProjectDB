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
                UserPasswordPasswordBox.Password = selectedUser.PasswordUser;
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
            if (UserPasswordPasswordBox.Visibility == Visibility.Visible)
            {
                UserPasswordTextBox.Text = UserPasswordPasswordBox.Password;
            }
            else
            {
                UserPasswordPasswordBox.Password = UserPasswordTextBox.Text;
            }
            string upperCaseLetters = "QWERTYUIOPASDFGHJKLZXCVBNMЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ",
                lowerCaseLetters = "qwertyuiopasdfghjklzxcvbnmйцукенгшщзхъфывапролджэячсмитьбю",
                numbers = "1234567890",
                enteredPassword = UserPasswordTextBox.Text;
            
            if (upperCaseLetters.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                ValidationErrorMsg.Text = "Некорректный пароль";
                ValidationErrorMsg.Visibility = Visibility.Visible;
                return;
            }
            else if (lowerCaseLetters.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                ValidationErrorMsg.Text = "Некорректный пароль";
                ValidationErrorMsg.Visibility = Visibility.Visible;
                return;
            }
            else if (numbers.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                ValidationErrorMsg.Text = "Некорректный пароль";
                ValidationErrorMsg.Visibility = Visibility.Visible;
                return;
            }
            else if (enteredPassword.Length < 8)
            {
                ValidationErrorMsg.Text = "Некорректный пароль";
                ValidationErrorMsg.Visibility = Visibility.Visible;
                return;
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
            if ((DBEntities.GetContext().User.FirstOrDefault(u => u.LoginUser == UserLoginTextBox.Text) == null ||
                    _currentUser.LoginUser == UserLoginTextBox.Text))
            {
                InvalidUserLoginError.Visibility = Visibility.Collapsed;
                userSaveButton.IsEnabled = true;
            }
            else
            {
                InvalidUserLoginError.Visibility = Visibility.Visible;
                userSaveButton.IsEnabled = false;
            }
            if (string.IsNullOrWhiteSpace(UserLoginTextBox.Text))
            {
                InvalidUserLoginError.Visibility = Visibility.Collapsed;
            }
            if (string.IsNullOrWhiteSpace(UserLoginTextBox.Text) |
               string.IsNullOrWhiteSpace(UserPasswordTextBox.Text) |
               UserRoleComboBox.SelectedValue == null |
               InvalidUserPasswordError.Visibility == Visibility.Visible |
               InvalidUserLoginError.Visibility == Visibility.Visible)
                userSaveButton.IsEnabled = false;
        }

        private void UserPasswordTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string upperCaseLetters = "QWERTYUIOPASDFGHJKLZXCVBNMЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ",
                lowerCaseLetters = "qwertyuiopasdfghjklzxcvbnmйцукенгшщзхъфывапролджэячсмитьбю",
                numbers = "1234567890",
                enteredPassword = UserPasswordTextBox.Text;

            if (upperCaseLetters.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                InvalidUserPasswordError.Visibility = Visibility.Visible;
                userSaveButton.IsEnabled = false;
            }
            else if (lowerCaseLetters.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                InvalidUserPasswordError.Visibility = Visibility.Visible;
                userSaveButton.IsEnabled = false;
            }
            else if (numbers.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                InvalidUserPasswordError.Visibility = Visibility.Visible;
                userSaveButton.IsEnabled = false;
            }
            else if (enteredPassword.Length < 8)
            {
                InvalidUserPasswordError.Visibility = Visibility.Visible;
                userSaveButton.IsEnabled = false;
            }
            else
            {
                InvalidUserPasswordError.Visibility = Visibility.Collapsed;
                userSaveButton.IsEnabled = true;
            }
            if (string.IsNullOrWhiteSpace(UserPasswordTextBox.Text))
            {
                UserPasswordPasswordBox.Password = null;
                InvalidUserPasswordError.Visibility = Visibility.Collapsed;
            }
            if (string.IsNullOrWhiteSpace(UserLoginTextBox.Text) |
               string.IsNullOrWhiteSpace(UserPasswordTextBox.Text) |
               UserRoleComboBox.SelectedValue == null |
               InvalidUserPasswordError.Visibility == Visibility.Visible |
               InvalidUserLoginError.Visibility == Visibility.Visible)
                userSaveButton.IsEnabled = false;
        }

        private void UserRoleComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserLoginTextBox.Text) |
               string.IsNullOrWhiteSpace(UserPasswordTextBox.Text) |
               UserRoleComboBox.SelectedValue == null |
               InvalidUserPasswordError.Visibility == Visibility.Visible |
               InvalidUserLoginError.Visibility == Visibility.Visible)
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
            Keyboard.ClearFocus();
        }

        private void CancelChangesButton_Click(object sender, RoutedEventArgs e)
        {
            UserLoginTextBox.Text = _currentUser.LoginUser;
            UserPasswordPasswordBox.Password = _currentUser.PasswordUser;
            UserPasswordTextBox.Text = _currentUser.PasswordUser;
            UserRoleComboBox.SelectedItem = _currentUser.Role;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Keyboard.ClearFocus();
        }

        private void VisibilityPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserPasswordPasswordBox.Visibility == Visibility.Visible)
            {
                UserPasswordTextBox.Text = UserPasswordPasswordBox.Password;
                VisiblePasswordIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.EyeOffOutline;
                VisiblePasswordIcon.Margin = new Thickness(0, 1.5, 0, 0);
                UserPasswordTextBox.Visibility = Visibility.Visible;
                UserPasswordPasswordBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                UserPasswordPasswordBox.Password = UserPasswordTextBox.Text;
                VisiblePasswordIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.EyeOutline;
                VisiblePasswordIcon.Margin = new Thickness(0, 0, 0, 0);
                UserPasswordPasswordBox.Visibility = Visibility.Visible;
                UserPasswordTextBox.Visibility = Visibility.Collapsed;
            }
        }

        private void UserPasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            string upperCaseLetters = "QWERTYUIOPASDFGHJKLZXCVBNMЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ",
                lowerCaseLetters = "qwertyuiopasdfghjklzxcvbnmйцукенгшщзхъфывапролджэячсмитьбю",
                numbers = "1234567890",
                enteredPassword = UserPasswordPasswordBox.Password;

            if (upperCaseLetters.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                InvalidUserPasswordError.Visibility = Visibility.Visible;
                userSaveButton.IsEnabled = false;
            }
            else if (lowerCaseLetters.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                InvalidUserPasswordError.Visibility = Visibility.Visible;
                userSaveButton.IsEnabled = false;
            }
            else if (numbers.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                InvalidUserPasswordError.Visibility = Visibility.Visible;
                userSaveButton.IsEnabled = false;
            }
            else if (enteredPassword.Length < 8)
            {
                InvalidUserPasswordError.Visibility = Visibility.Visible;
                userSaveButton.IsEnabled = false;
            }
            else
            {
                InvalidUserPasswordError.Visibility = Visibility.Collapsed;
                userSaveButton.IsEnabled = true;
            }
            if (string.IsNullOrWhiteSpace(UserPasswordPasswordBox.Password))
            {
                UserPasswordTextBox.Text = null;
                InvalidUserPasswordError.Visibility = Visibility.Collapsed;
            }
            else
            {
                UserPasswordTextBox.Text = UserPasswordPasswordBox.Password;
            }
            if (string.IsNullOrWhiteSpace(UserLoginTextBox.Text) |
               string.IsNullOrWhiteSpace(UserPasswordTextBox.Text) |
               UserRoleComboBox.SelectedValue == null |
               InvalidUserPasswordError.Visibility == Visibility.Visible |
               InvalidUserLoginError.Visibility == Visibility.Visible)
                userSaveButton.IsEnabled = false;
        }
    }
}
