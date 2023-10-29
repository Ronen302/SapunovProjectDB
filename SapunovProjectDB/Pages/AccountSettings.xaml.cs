using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SapunovProjectDB.Pages
{
    public partial class AccountSettings : Page
    {
        User _currentUser = DBEntities.GetContext().User
                .FirstOrDefault(u => u.IdUser == Properties.Settings.Default.CurrentIdUser);
        public AccountSettings()
        {
            InitializeComponent();
            UserLoginTextBox.Text = Properties.Settings.Default.CurrentUser;
        }

        private void userSaveButton_Click(object sender, RoutedEventArgs e)
        {
            string upperCaseLetters = "QWERTYUIOPASDFGHJKLZXCVBNMЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ",
                lowerCaseLetters = "qwertyuiopasdfghjklzxcvbnmйцукенгшщзхъфывапролджэячсмитьбю",
                numbers = "1234567890",
                enteredPassword = UserNewPasswordTextBox.Password;
            if(string.IsNullOrWhiteSpace(UserLoginTextBox.Text))
            {
                InvalidNewPaswwordError.Text = "Пароль должен содержать заглавные буквы";
            }
            else
            {
                try
                {
                    if(DBEntities.GetContext().User
                        .FirstOrDefault(u => u.LoginUser == UserLoginTextBox.Text) == null)
                    {
                        _currentUser.LoginUser = UserLoginTextBox.Text;
                        DBEntities.GetContext().SaveChanges();
                        Properties.Settings.Default.CurrentUser = UserLoginTextBox.Text;
                        Properties.Settings.Default.Save();
                        DataIsSaved();
                    }
                }
                catch (Exception ex)
                {
                    Error.ErrorMB(ex);
                }
            }

            if (!string.IsNullOrWhiteSpace(UserNewPasswordTextBox.Password))
            {
                if (upperCaseLetters.IndexOfAny(enteredPassword.ToCharArray()) == -1)
                {
                    InvalidNewPaswwordError.Text = "Пароль должен содержать заглавные буквы";
                    InvalidNewPaswwordError.Visibility = Visibility.Visible;
                }
                else if (lowerCaseLetters.IndexOfAny(enteredPassword.ToCharArray()) == -1)
                {
                    InvalidNewPaswwordError.Text = "Пароль должен содержать маленькие буквы";
                    InvalidNewPaswwordError.Visibility = Visibility.Visible;
                }
                else if (numbers.IndexOfAny(enteredPassword.ToCharArray()) == -1)
                {
                    InvalidNewPaswwordError.Text = "Пароль должен содержать цифры";
                    InvalidNewPaswwordError.Visibility = Visibility.Visible;
                }
                else if (enteredPassword.Length > 30)
                {
                    InvalidNewPaswwordError.Text = "Пароль должен быть не более 30 символов";
                    InvalidNewPaswwordError.Visibility = Visibility.Visible;
                }
                else if (_currentUser.PasswordUser != UserCurrentPasswordTextBox.Password)
                {
                    InvalidCurrentPaswwordError.Text = "Неверный пароль";
                    InvalidCurrentPaswwordError.Visibility = Visibility.Visible;
                }
                else
                {
                    try
                    {
                        _currentUser.PasswordUser = UserNewPasswordTextBox.Password;
                        DBEntities.GetContext().SaveChanges();
                        DataIsSaved();
                    }
                    catch (Exception ex)
                    {
                        Error.ErrorMB(ex);
                    }
                }
            }
        }
        private void UserLoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DBEntities.GetContext().User
                .FirstOrDefault(u => u.LoginUser == UserLoginTextBox.Text) != null |
                string.IsNullOrWhiteSpace(UserLoginTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void UserCurrentPasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserCurrentPasswordTextBox.Password) |
                string.IsNullOrWhiteSpace(UserNewPasswordTextBox.Password))
            {
                if (DBEntities.GetContext().User
                .FirstOrDefault(u => u.LoginUser == UserLoginTextBox.Text) != null |
                string.IsNullOrWhiteSpace(UserLoginTextBox.Text))
                {
                    userSaveButton.IsEnabled = false;
                }
                else
                {
                    userSaveButton.IsEnabled = true;
                }
            }
            else
                userSaveButton.IsEnabled = true;
        }

        private void UserNewPasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserCurrentPasswordTextBox.Password) |
                string.IsNullOrWhiteSpace(UserNewPasswordTextBox.Password))
            {
                if (DBEntities.GetContext().User
                .FirstOrDefault(u => u.LoginUser == UserLoginTextBox.Text) != null |
                string.IsNullOrWhiteSpace(UserLoginTextBox.Text))
                {
                    userSaveButton.IsEnabled = false;
                }
                else
                {
                    userSaveButton.IsEnabled = true;
                }
            }
            else
                userSaveButton.IsEnabled = true;
        }

        private void UserCurrentPasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InvalidCurrentPaswwordError.Visibility = Visibility.Collapsed;
        }

        private void UserNewPasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InvalidNewPaswwordError.Visibility = Visibility.Collapsed;
        }
        private async void DataIsSaved()
        {
            dataIsSavedMessage.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(2.6));
            dataIsSavedMessage.Visibility = Visibility.Collapsed;
        }
    }
}
