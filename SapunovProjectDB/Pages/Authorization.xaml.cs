using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using SapunovProjectDB.Pages;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SapunovProjectDB.Pages
{
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.SaveLogin))
            {
                AuthLoginTb.Focus();
            }

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.SaveLogin))
            {
                SaveLoginCb.IsChecked = true;
                AuthPasswordPb.Focus();
            }
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Registration());
        }

        private void PasswordTb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AuthPasswordPb.Focus();
        }

        private void PasswordPb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(AuthPasswordPb.Password) && AuthPasswordPb.Password.Length > 0)
            {
                PasswordTb.Visibility = Visibility.Collapsed;
            }
            else
            {
                PasswordTb.Visibility = Visibility.Visible;
            }
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AuthLoginTb.Text) && string.IsNullOrWhiteSpace(AuthPasswordPb.Password))
            {
                EmptyLoginError.Visibility = Visibility.Visible;
                InvalidPaswwordError.Text = "Не указан пароль";
                InvalidPaswwordError.Visibility = Visibility.Visible;
            }
            else if (string.IsNullOrWhiteSpace(AuthLoginTb.Text))
            {
                EmptyLoginError.Text = "Не указан логин";
                EmptyLoginError.Visibility = Visibility.Visible;
            }
            else if (string.IsNullOrWhiteSpace(AuthPasswordPb.Password))
            {
                InvalidPaswwordError.Text = "Не указан пароль";
                InvalidPaswwordError.Visibility = Visibility.Visible;
            }
            else
            {
                try
                {
                    var user = DBEntities.GetContext()
                        .User
                        .FirstOrDefault(u => u.LoginUser == AuthLoginTb.Text);

                    if (user == null)
                    {
                        EmptyLoginError.Text = "Такого пользователя не существует";
                        EmptyLoginError.Visibility = Visibility.Visible;
                        return;
                    }
                    if (user.PasswordUser != AuthPasswordPb.Password)
                    {
                        InvalidPaswwordError.Text = "Неверный пароль";
                        InvalidPaswwordError.Visibility = Visibility.Visible;
                        return;
                    }
                    else
                    {
                        Properties.Settings.Default.CurrentUser = AuthLoginTb.Text;
                        Properties.Settings.Default.Save();

                        if (SaveLoginCb.IsChecked == true)
                        {
                            Properties.Settings.Default.SaveLogin = AuthLoginTb.Text;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            Properties.Settings.Default.SaveLogin = "";
                            Properties.Settings.Default.Save();
                        }
                        switch (user.IdRole)
                        {
                            case 1:
                                Properties.Settings.Default.UserRole = "Администратор";
                                Properties.Settings.Default.Save();
                                NavigationService.Navigate(new MainPage());
                                break;
                            case 2:
                                Properties.Settings.Default.UserRole = "Менеджер";
                                Properties.Settings.Default.Save();
                                NavigationService.Navigate(new MainPage());
                                break;
                            case 3:
                                Properties.Settings.Default.UserRole = "Сотрудник";
                                Properties.Settings.Default.Save();
                                NavigationService.Navigate(new MainPage());
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error.ErrorMB(ex);
                }
            }
        }

        private void AuthPasswordPb_GotFocus(object sender, RoutedEventArgs e)
        {
            InvalidPaswwordError.Visibility = Visibility.Collapsed;
        }

        private void AuthLoginTb_GotFocus(object sender, RoutedEventArgs e)
        {
            EmptyLoginError.Visibility = Visibility.Collapsed;
        }

        private void changeTheme_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ColorMode = "Dark";
            Properties.Settings.Default.Save();
        }

        private void changeThemeLight_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ColorMode = "Light";
            Properties.Settings.Default.Save();
        }
    }
}
