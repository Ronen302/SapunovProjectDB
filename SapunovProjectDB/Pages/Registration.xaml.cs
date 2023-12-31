﻿using SapunovProjectDB.Classes;
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
        private readonly Client _currentClient = new Client();
        private readonly User _currentUser = new User();
        public Registration()
        {
            InitializeComponent();
            RegLoginTb.Focus();
        }

        private void RegPasswordTb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RegPasswordPb.Focus();
        }

        private void RegPasswordPb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(RegPasswordPb.Password) && RegPasswordPb.Password.Length > 0)
            {
                RegPasswordTb.Visibility = Visibility.Collapsed;
            }
            else
            {
                RegPasswordTb.Visibility = Visibility.Visible;
            }
            if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                string.IsNullOrWhiteSpace(RegPasswordPb.Password) |
                string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                string.IsNullOrWhiteSpace(RegNameTb.Text) |
                string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                string.IsNullOrWhiteSpace(RegEmailTb.Text))
                RegistrationBtn.IsEnabled = false;
            else
                RegistrationBtn.IsEnabled = true;
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
            if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                string.IsNullOrWhiteSpace(RegPasswordPb.Password) |
                string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                string.IsNullOrWhiteSpace(RegNameTb.Text) |
                string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                string.IsNullOrWhiteSpace(RegEmailTb.Text))
                RegistrationBtn.IsEnabled = false;
            else
                RegistrationBtn.IsEnabled = true;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Authorization());
        }

        private async void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            string upperCaseLetters = "QWERTYUIOPASDFGHJKLZXCVBNMЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ",
                lowerCaseLetters = "qwertyuiopasdfghjklzxcvbnmйцукенгшщзхъфывапролджэячсмитьбю",
                numbers = "1234567890",
                enteredPassword = RegPasswordPb.Password;

            if (DBEntities.GetContext().User.FirstOrDefault(u => u.LoginUser == RegLoginTb.Text) != null)
            {
                EmptyRegLoginError.Text = "Такой логин уже существует";
                EmptyRegLoginError.Visibility = Visibility.Visible;
            }
            else if (upperCaseLetters.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                EmptyPasswordError.Text = "Пароль должен содержать заглавные буквы";
                EmptyPasswordError.Visibility = Visibility.Visible;
            }
            else if (lowerCaseLetters.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                EmptyPasswordError.Text = "Пароль должен содержать маленькие буквы";
                EmptyPasswordError.Visibility = Visibility.Visible;
            }
            else if (numbers.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                EmptyPasswordError.Text = "Пароль должен содержать цифры";
                EmptyPasswordError.Visibility = Visibility.Visible;
            }
            else if (enteredPassword.Length > 30)
            {
                EmptyPasswordError.Text = "Пароль должен быть не более 30 символов";
                EmptyPasswordError.Visibility = Visibility.Visible;
            }
            else if (RegPasswordPb.Password != RegRepeatPasswordPb.Password)
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

                    User newUser = new User()
                    {
                        LoginUser = RegLoginTb.Text,
                        PasswordUser = RegPasswordPb.Password,
                        IdRole = 4
                    };
                    DBEntities.GetContext().User.Add(newUser);
                    DBEntities.GetContext().SaveChanges();
                    _currentUser.IdUser = newUser.IdUser;

                    Client newClient = new Client()
                    {
                        NameClient = RegNameTb.Text,
                        PhoneNumberClient = RegPhoneTb.Text,
                        EmailClient = RegEmailTb.Text,
                        DateOfRegistration = DateTime.Now,
                        IdUser = _currentUser.IdUser
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

        private void RegPasswordPb_GotFocus(object sender, RoutedEventArgs e)
        {
            EmptyPasswordError.Visibility = Visibility.Collapsed;
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

        private void RegNameTb_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                string.IsNullOrWhiteSpace(RegPasswordPb.Password) |
                string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                string.IsNullOrWhiteSpace(RegNameTb.Text) |
                string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                string.IsNullOrWhiteSpace(RegEmailTb.Text))
                RegistrationBtn.IsEnabled = false;
            else
                RegistrationBtn.IsEnabled = true;
        }

        private void RegPhoneTb_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                string.IsNullOrWhiteSpace(RegPasswordPb.Password) |
                string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                string.IsNullOrWhiteSpace(RegNameTb.Text) |
                string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                string.IsNullOrWhiteSpace(RegEmailTb.Text))
                RegistrationBtn.IsEnabled = false;
            else
                RegistrationBtn.IsEnabled = true;
        }

        private void RegEmailTb_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                string.IsNullOrWhiteSpace(RegPasswordPb.Password) |
                string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                string.IsNullOrWhiteSpace(RegNameTb.Text) |
                string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                string.IsNullOrWhiteSpace(RegEmailTb.Text))
                RegistrationBtn.IsEnabled = false;
            else
                RegistrationBtn.IsEnabled = true;
        }

        private void RegLoginTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                string.IsNullOrWhiteSpace(RegPasswordPb.Password) |
                string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                string.IsNullOrWhiteSpace(RegNameTb.Text) |
                string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                string.IsNullOrWhiteSpace(RegEmailTb.Text))
                RegistrationBtn.IsEnabled = false;
            else
                RegistrationBtn.IsEnabled = true;
        }

        private void RegNameTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                string.IsNullOrWhiteSpace(RegPasswordPb.Password) |
                string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                string.IsNullOrWhiteSpace(RegNameTb.Text) |
                string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                string.IsNullOrWhiteSpace(RegEmailTb.Text))
                RegistrationBtn.IsEnabled = false;
            else
                RegistrationBtn.IsEnabled = true;
        }

        private void RegPhoneTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                string.IsNullOrWhiteSpace(RegPasswordPb.Password) |
                string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                string.IsNullOrWhiteSpace(RegNameTb.Text) |
                string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                string.IsNullOrWhiteSpace(RegEmailTb.Text))
                RegistrationBtn.IsEnabled = false;
            else
                RegistrationBtn.IsEnabled = true;
        }

        private void RegEmailTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RegLoginTb.Text) |
                string.IsNullOrWhiteSpace(RegPasswordPb.Password) |
                string.IsNullOrWhiteSpace(RegRepeatPasswordPb.Password) |
                string.IsNullOrWhiteSpace(RegNameTb.Text) |
                string.IsNullOrWhiteSpace(RegPhoneTb.Text) |
                string.IsNullOrWhiteSpace(RegEmailTb.Text))
                RegistrationBtn.IsEnabled = false;
            else
                RegistrationBtn.IsEnabled = true;
        }
    }
}
