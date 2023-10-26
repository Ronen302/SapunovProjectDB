using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
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

            if (Properties.Settings.Default.IsLoginSaved == false)
            {
                AuthLoginTb.Focus();
            }

            if (Properties.Settings.Default.IsLoginSaved)
            {
                SaveLoginCb.IsChecked = true;
                AuthLoginTb.Text = Properties.Settings.Default.CurrentUser;
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
            if (string.IsNullOrWhiteSpace(AuthLoginTb.Text) |
                string.IsNullOrWhiteSpace(AuthPasswordPb.Password))
                LoginBtn.IsEnabled = false;
            else
                LoginBtn.IsEnabled = true;
        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
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
                    User user = DBEntities.GetContext()
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
                            Properties.Settings.Default.IsLoginSaved = true;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            Properties.Settings.Default.IsLoginSaved = false;
                            Properties.Settings.Default.Save();
                        }
                        int _currentIdUser = DBEntities.GetContext().User
                            .FirstOrDefault(u => u.LoginUser == Properties.Settings.Default.CurrentUser).IdUser;
                        string _currentNameClient = DBEntities.GetContext().Client
                            .FirstOrDefault(u => u.IdUser == _currentIdUser).NameClient;
                        WelcomeMessage.Text = $"Здравствуйте, {_currentNameClient}!";
                        MainAuthBorder.Visibility = Visibility.Collapsed;
                        WelcomeMessage.Visibility = Visibility.Visible;
                        await Task.Delay(TimeSpan.FromSeconds(2.6));
                        WelcomeMessage.Visibility = Visibility.Collapsed;
                        switch (user.IdRole)
                        {
                            case 1:
                                Properties.Settings.Default.UserRole = 1;
                                Properties.Settings.Default.Save();
                                NavigationService.Navigate(new MainPage());
                                break;
                            case 2:
                                Properties.Settings.Default.UserRole = 2;
                                Properties.Settings.Default.Save();
                                NavigationService.Navigate(new MainPage());
                                break;
                            case 3:
                                Properties.Settings.Default.UserRole = 3;
                                Properties.Settings.Default.Save();
                                NavigationService.Navigate(new MainPage());
                                break;
                            case 4:
                                Properties.Settings.Default.UserRole = 4;
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

        private void MainAuthStackPannel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainAuthBorder.Height = MainAuthStackPannel.ActualHeight;
        }

        private void AuthLoginTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AuthLoginTb.Text) |
                string.IsNullOrWhiteSpace(AuthPasswordPb.Password))
                LoginBtn.IsEnabled = false;
            else
                LoginBtn.IsEnabled = true;
        }
    }
}
