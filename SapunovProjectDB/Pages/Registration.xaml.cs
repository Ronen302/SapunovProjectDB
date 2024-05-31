using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using SapunovProjectDB.Windows;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace SapunovProjectDB.Pages
{
    public partial class Registration : Page
    {
        User _newUser = new User();
        public Registration()
        {
            InitializeComponent();
            VisibleRegPasswordTb.Visibility = Visibility.Collapsed;
            RegLoginTb.Focus();
        }

        private void RegPasswordTb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RegPasswordPb.Focus();
        }

        private void RegPasswordPb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            string upperCaseLetters = "QWERTYUIOPASDFGHJKLZXCVBNMЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ",
                lowerCaseLetters = "qwertyuiopasdfghjklzxcvbnmйцукенгшщзхъфывапролджэячсмитьбю",
                numbers = "1234567890",
                enteredPassword = RegPasswordPb.Password;

            if (!string.IsNullOrWhiteSpace(RegPasswordPb.Password) && RegPasswordPb.Password.Length > 0)
            {
                RegPasswordTb.Visibility = Visibility.Collapsed;
                VisibleRegPasswordTb.Visibility = Visibility.Collapsed;
            }
            else
            {
                RegPasswordTb.Visibility = Visibility.Visible;
            }

            if (upperCaseLetters.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                EmptyPasswordError.Visibility = Visibility.Visible;
                RegistrationBtn.IsEnabled = false;
            }
            else if (lowerCaseLetters.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                EmptyPasswordError.Visibility = Visibility.Visible;
                RegistrationBtn.IsEnabled = false;
            }
            else if (numbers.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                EmptyPasswordError.Visibility = Visibility.Visible;
                RegistrationBtn.IsEnabled = false;
            }
            else if (enteredPassword.Length < 8)
            {
                EmptyPasswordError.Visibility = Visibility.Visible;
                RegistrationBtn.IsEnabled = false;
            }
            else
            {
                EmptyPasswordError.Visibility = Visibility.Collapsed;
                if (RegPasswordPb.Visibility == Visibility.Visible)
                {
                    if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                        string.IsNullOrWhiteSpace(RegPasswordPb.Password) |
                        string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                        string.IsNullOrWhiteSpace(RegNameTb.Text) |
                        string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                        string.IsNullOrWhiteSpace(RegEmailTb.Text) |
                        EmptyPasswordError.Visibility == Visibility.Visible)
                        RegistrationBtn.IsEnabled = false;
                    else
                        RegistrationBtn.IsEnabled = true;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                        string.IsNullOrWhiteSpace(VisibleRegPasswordTextBox.Text) |
                        string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                        string.IsNullOrWhiteSpace(RegNameTb.Text) |
                        string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                        string.IsNullOrWhiteSpace(RegEmailTb.Text) |
                        EmptyPasswordError.Visibility == Visibility.Visible)
                        RegistrationBtn.IsEnabled = false;
                    else
                        RegistrationBtn.IsEnabled = true;
                }
            }
            if (string.IsNullOrWhiteSpace(RegPasswordPb.Password))
                EmptyPasswordError.Visibility = Visibility.Collapsed;
        }

        private void RegRepeatPasswordTb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RegRepeatPasswordPb.Focus();
        }

        private void RegRepeatPasswordPb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) && RegRepeatPasswordPb.Password.Length > 0)
            {
                RegRepeatPasswordTb.Visibility = Visibility.Collapsed;
            }
            else
            {
                RegRepeatPasswordTb.Visibility = Visibility.Visible;
            }
            if (RegPasswordPb.Visibility == Visibility.Visible)
            {
                if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                    string.IsNullOrWhiteSpace(RegPasswordPb.Password) |
                    string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                    string.IsNullOrWhiteSpace(RegNameTb.Text) |
                    string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                    string.IsNullOrWhiteSpace(RegEmailTb.Text) |
                    EmptyPasswordError.Visibility == Visibility.Visible)
                    RegistrationBtn.IsEnabled = false;
                else
                    RegistrationBtn.IsEnabled = true;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                    string.IsNullOrWhiteSpace(VisibleRegPasswordTextBox.Text) |
                    string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                    string.IsNullOrWhiteSpace(RegNameTb.Text) |
                    string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                    string.IsNullOrWhiteSpace(RegEmailTb.Text) |
                    EmptyPasswordError.Visibility == Visibility.Visible)
                    RegistrationBtn.IsEnabled = false;
                else
                    RegistrationBtn.IsEnabled = true;
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Authorization());
        }

        private async void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DBEntities.GetContext().User.FirstOrDefault(u => u.LoginUser == RegLoginTb.Text) != null)
            {
                EmptyRegLoginError.Text = "Такой логин уже существует";
                EmptyRegLoginError.Visibility = Visibility.Visible;
            }
            else if (RegPasswordPb.Password != RegRepeatPasswordPb.Password &&
                VisibleRegPasswordTextBox.Text != RegRepeatPasswordPb.Password)
            {
                EmptyRepeatPasswordError.Text = "Пароли не совпадают";
                EmptyRepeatPasswordError.Visibility = Visibility.Visible;
            }
            else
            {
                try
                {
                    Properties.Settings.Default.CurrentUser = RegLoginTb.Text;
                    Properties.Settings.Default.Save();
                    if (SaveLoginCb.IsChecked == true)
                    {
                        Properties.Settings.Default.IsLoginSaved = true;
                        Properties.Settings.Default.SavedLoginUser = RegLoginTb.Text;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        Properties.Settings.Default.IsLoginSaved = false;
                        Properties.Settings.Default.SavedLoginUser = "";
                        Properties.Settings.Default.Save();
                    }

                    _newUser.LoginUser = RegLoginTb.Text;
                    if (RegPasswordPb.Visibility == Visibility.Visible)
                    {
                        _newUser.PasswordUser = RegPasswordPb.Password;
                    }
                    else
                    {
                        _newUser.PasswordUser = VisibleRegPasswordTextBox.Text;
                    }
                    _newUser.IdRole = 4;
                    DBEntities.GetContext().User.Add(_newUser);
                    DBEntities.GetContext().SaveChanges();

                    Client newClient = new Client()
                    {
                        NameClient = RegNameTb.Text,
                        PhoneNumberClient = RegPhoneTb.Text,
                        EmailClient = RegEmailTb.Text,
                        DateOfRegistration = DateTime.Now,
                        IdUser = _newUser.IdUser
                    };
                    DBEntities.GetContext().Client.Add(newClient);
                    DBEntities.GetContext().SaveChanges();
                    WelcomeMessage.Text = $"Добро пожаловать, {RegNameTb.Text}!";
                    MainRegBorder.Visibility = Visibility.Collapsed;
                    WelcomeMessage.Visibility = Visibility.Visible;
                    await Task.Delay(TimeSpan.FromSeconds(2.6));
                    WelcomeMessage.Visibility = Visibility.Collapsed;
                    User user = DBEntities.GetContext().User
                        .FirstOrDefault(u => u.LoginUser == RegLoginTb.Text);
                    Properties.Settings.Default.CurrentIdUser = user.IdUser;
                    Properties.Settings.Default.Save();
                    user.DateOfLastVisit = DateTime.Now;
                    DBEntities.GetContext().SaveChanges();
                    switch (user.IdRole)
                    {
                        case 4:
                            Properties.Settings.Default.UserRole = 4;
                            Properties.Settings.Default.Save();
                            NavigationService.Navigate(new MainPage());
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Error.ErrorMB(ex);
                }
            }
        }

        private void RegLoginTb_GotFocus(object sender, RoutedEventArgs e)
        {
            EmptyRegLoginError.Visibility = Visibility.Collapsed;
        }

        private void RegRepeatPasswordPb_GotFocus(object sender, RoutedEventArgs e)
        {
            EmptyRepeatPasswordError.Visibility = Visibility.Collapsed;
        }

        private void MainStackPannel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainRegBorder.Height = MainStackPannel.ActualHeight;
        }

        private void RegLoginTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (RegPasswordPb.Visibility == Visibility.Visible)
            {
                if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                    string.IsNullOrWhiteSpace(RegPasswordPb.Password) |
                    string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                    string.IsNullOrWhiteSpace(RegNameTb.Text) |
                    string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                    string.IsNullOrWhiteSpace(RegEmailTb.Text) |
                    EmptyPasswordError.Visibility == Visibility.Visible)
                    RegistrationBtn.IsEnabled = false;
                else
                    RegistrationBtn.IsEnabled = true;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                    string.IsNullOrWhiteSpace(VisibleRegPasswordTextBox.Text) |
                    string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                    string.IsNullOrWhiteSpace(RegNameTb.Text) |
                    string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                    string.IsNullOrWhiteSpace(RegEmailTb.Text) |
                    EmptyPasswordError.Visibility == Visibility.Visible)
                    RegistrationBtn.IsEnabled = false;
                else
                    RegistrationBtn.IsEnabled = true;
            }
        }

        private void RegNameTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (RegPasswordPb.Visibility == Visibility.Visible)
            {
                if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                    string.IsNullOrWhiteSpace(RegPasswordPb.Password) |
                    string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                    string.IsNullOrWhiteSpace(RegNameTb.Text) |
                    string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                    string.IsNullOrWhiteSpace(RegEmailTb.Text) |
                    EmptyPasswordError.Visibility == Visibility.Visible)
                    RegistrationBtn.IsEnabled = false;
                else
                    RegistrationBtn.IsEnabled = true;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                    string.IsNullOrWhiteSpace(VisibleRegPasswordTextBox.Text) |
                    string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                    string.IsNullOrWhiteSpace(RegNameTb.Text) |
                    string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                    string.IsNullOrWhiteSpace(RegEmailTb.Text) |
                    EmptyPasswordError.Visibility == Visibility.Visible)
                    RegistrationBtn.IsEnabled = false;
                else
                    RegistrationBtn.IsEnabled = true;
            }
        }

        private void RegPhoneTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (RegPasswordPb.Visibility == Visibility.Visible)
            {
                if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                    string.IsNullOrWhiteSpace(RegPasswordPb.Password) |
                    string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                    string.IsNullOrWhiteSpace(RegNameTb.Text) |
                    string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                    string.IsNullOrWhiteSpace(RegEmailTb.Text) |
                    EmptyPasswordError.Visibility == Visibility.Visible)
                    RegistrationBtn.IsEnabled = false;
                else
                    RegistrationBtn.IsEnabled = true;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                    string.IsNullOrWhiteSpace(VisibleRegPasswordTextBox.Text) |
                    string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                    string.IsNullOrWhiteSpace(RegNameTb.Text) |
                    string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                    string.IsNullOrWhiteSpace(RegEmailTb.Text) |
                    EmptyPasswordError.Visibility == Visibility.Visible)
                    RegistrationBtn.IsEnabled = false;
                else
                    RegistrationBtn.IsEnabled = true;
            }
        }

        private void RegEmailTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (RegPasswordPb.Visibility == Visibility.Visible)
            {
                if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                    string.IsNullOrWhiteSpace(RegPasswordPb.Password) |
                    string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                    string.IsNullOrWhiteSpace(RegNameTb.Text) |
                    string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                    string.IsNullOrWhiteSpace(RegEmailTb.Text) |
                    EmptyPasswordError.Visibility == Visibility.Visible)
                    RegistrationBtn.IsEnabled = false;
                else
                    RegistrationBtn.IsEnabled = true;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                    string.IsNullOrWhiteSpace(VisibleRegPasswordTextBox.Text) |
                    string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                    string.IsNullOrWhiteSpace(RegNameTb.Text) |
                    string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                    string.IsNullOrWhiteSpace(RegEmailTb.Text) |
                    EmptyPasswordError.Visibility == Visibility.Visible)
                    RegistrationBtn.IsEnabled = false;
                else
                    RegistrationBtn.IsEnabled = true;
            }
        }

        private void VisibleRegPasswordTb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VisibleRegPasswordTextBox.Focus();
        }

        private void VisibleRegPasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string upperCaseLetters = "QWERTYUIOPASDFGHJKLZXCVBNMЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ",
                lowerCaseLetters = "qwertyuiopasdfghjklzxcvbnmйцукенгшщзхъфывапролджэячсмитьбю",
                numbers = "1234567890",
                enteredPassword = VisibleRegPasswordTextBox.Text;

            if (!string.IsNullOrWhiteSpace(VisibleRegPasswordTextBox.Text) && VisibleRegPasswordTextBox.Text.Length > 0)
            {
                RegPasswordTb.Visibility = Visibility.Collapsed;
                VisibleRegPasswordTb.Visibility = Visibility.Collapsed;
            }
            else
            {
                VisibleRegPasswordTb.Visibility = Visibility.Visible;
            }
            if (upperCaseLetters.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                EmptyPasswordError.Visibility = Visibility.Visible;
                RegistrationBtn.IsEnabled = false;
            }
            else if (lowerCaseLetters.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                EmptyPasswordError.Visibility = Visibility.Visible;
                RegistrationBtn.IsEnabled = false;
            }
            else if (numbers.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                EmptyPasswordError.Visibility = Visibility.Visible;
                RegistrationBtn.IsEnabled = false;
            }
            else if (enteredPassword.Length < 8)
            {
                EmptyPasswordError.Visibility = Visibility.Visible;
                RegistrationBtn.IsEnabled = false;
            }
            else
            {
                EmptyPasswordError.Visibility = Visibility.Collapsed;
                if (RegPasswordPb.Visibility == Visibility.Visible)
                {
                    if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                        string.IsNullOrWhiteSpace(RegPasswordPb.Password) |
                        string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                        string.IsNullOrWhiteSpace(RegNameTb.Text) |
                        string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                        string.IsNullOrWhiteSpace(RegEmailTb.Text) |
                        EmptyPasswordError.Visibility == Visibility.Visible)
                        RegistrationBtn.IsEnabled = false;
                    else
                        RegistrationBtn.IsEnabled = true;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                        string.IsNullOrWhiteSpace(VisibleRegPasswordTextBox.Text) |
                        string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                        string.IsNullOrWhiteSpace(RegNameTb.Text) |
                        string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                        string.IsNullOrWhiteSpace(RegEmailTb.Text) |
                        EmptyPasswordError.Visibility == Visibility.Visible)
                        RegistrationBtn.IsEnabled = false;
                    else
                        RegistrationBtn.IsEnabled = true;
                }
            }
            if (string.IsNullOrWhiteSpace(VisibleRegPasswordTextBox.Text))
                EmptyPasswordError.Visibility = Visibility.Collapsed;
        }

        private void VisibilityPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegPasswordPb.Visibility == Visibility.Visible)
            {
                VisibleRegPasswordTextBox.Text = RegPasswordPb.Password;
                VisiblePasswordIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.EyeOffOutline;
                VisiblePasswordIcon.Margin = new Thickness(0, 1.5, 0, 0);
                VisibleRegPasswordTextBox.Visibility = Visibility.Visible;
                RegPasswordPb.Visibility = Visibility.Collapsed;
            }
            else
            {
                RegPasswordPb.Password = VisibleRegPasswordTextBox.Text;
                VisiblePasswordIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.EyeOutline;
                VisiblePasswordIcon.Margin = new Thickness(0, 0, 0, 0);
                RegPasswordPb.Visibility = Visibility.Visible;
                VisibleRegPasswordTextBox.Visibility = Visibility.Collapsed;
            }
        }
    }
}
