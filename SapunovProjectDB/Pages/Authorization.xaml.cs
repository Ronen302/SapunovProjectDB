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
            else
            {
                SaveLoginCb.IsChecked = true;
                AuthLoginTb.Text = Properties.Settings.Default.SavedLoginUser;
                AuthPasswordPb.Focus();
            }
            VisiblePasswordTb.Visibility = Visibility.Collapsed;
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
                VisiblePasswordTb.Visibility = Visibility.Collapsed;
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
            try
            {
                User user = DBEntities.GetContext().User
                    .FirstOrDefault(u => u.LoginUser == AuthLoginTb.Text);

                if (user == null)
                {
                    EmptyLoginError.Text = "Такого пользователя не существует";
                    EmptyLoginError.Visibility = Visibility.Visible;
                    return;
                }
                if (user.PasswordUser != AuthPasswordPb.Password &&
                    user.PasswordUser != VisibleAuthPasswordTb.Text)
                {
                    InvalidPaswwordError.Text = "Неверный пароль";
                    InvalidPaswwordError.Visibility = Visibility.Visible;
                    return;
                }
                else
                {
                    TimeSpan _timeNow = DateTime.Now.TimeOfDay;
                    TimeSpan _timeMorning = new TimeSpan(06, 00, 00);
                    TimeSpan _timeDay = new TimeSpan(12, 00, 00);
                    TimeSpan _timeEvening = new TimeSpan(18, 00, 00);
                    TimeSpan _timeNight = new TimeSpan(23, 00, 00);

                    Properties.Settings.Default.CurrentUser = AuthLoginTb.Text;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.CurrentIdUser = user.IdUser;
                    Properties.Settings.Default.Save();
                    user.DateOfLastVisit = DateTime.Now;
                    DBEntities.GetContext().SaveChanges();
                    if (SaveLoginCb.IsChecked == true)
                    {
                        Properties.Settings.Default.IsLoginSaved = true;
                        Properties.Settings.Default.SavedLoginUser = AuthLoginTb.Text;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        Properties.Settings.Default.IsLoginSaved = false;
                        Properties.Settings.Default.SavedLoginUser = "";
                        Properties.Settings.Default.Save();
                    }
                    int _currentIdUser = Properties.Settings.Default.CurrentIdUser;
                    string _currentNameClient = DBEntities.GetContext().Client
                        .FirstOrDefault(u => u.IdUser == _currentIdUser).NameClient;
                    if (_timeNow >= _timeMorning && _timeNow < _timeDay)
                    {
                        WelcomeMessage.Text = $"Доброе утро, {_currentNameClient}!";
                    }
                    else if (_timeNow >= _timeDay && _timeNow < _timeEvening)
                    {
                        WelcomeMessage.Text = $"Добрый день, {_currentNameClient}!";
                    }
                    else if (_timeNow >= _timeEvening && _timeNow < _timeNight)
                    {
                        WelcomeMessage.Text = $"Добрый вечер, {_currentNameClient}!";
                    }
                    else
                    {
                        WelcomeMessage.Text = $"Доброй ночи, {_currentNameClient}!";
                    }
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

        private void VisibilityPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (AuthPasswordPb.Visibility == Visibility.Visible)
            {
                VisibleAuthPasswordTb.Text = AuthPasswordPb.Password;
                VisiblePasswordIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.EyeOffOutline;
                VisiblePasswordIcon.Margin = new Thickness(0, 1.5, 0, 0);
                VisibleAuthPasswordTb.Visibility = Visibility.Visible;
                AuthPasswordPb.Visibility = Visibility.Collapsed;
            }
            else
            {
                AuthPasswordPb.Password = VisibleAuthPasswordTb.Text;
                VisiblePasswordIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.EyeOutline;
                VisiblePasswordIcon.Margin = new Thickness(0, 0, 0, 0);
                AuthPasswordPb.Visibility = Visibility.Visible;
                VisibleAuthPasswordTb.Visibility = Visibility.Collapsed;
            }
        }

        private void VisiblePasswordTb_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VisibleAuthPasswordTb.Focus();
        }

        private void VisibleAuthPasswordTb_GotFocus(object sender, RoutedEventArgs e)
        {
            InvalidPaswwordError.Visibility = Visibility.Collapsed;
        }

        private void VisibleAuthPasswordTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(VisibleAuthPasswordTb.Text) && VisibleAuthPasswordTb.Text.Length > 0)
            {
                VisiblePasswordTb.Visibility = Visibility.Collapsed;
                PasswordTb.Visibility = Visibility.Collapsed;
            }
            else
            {
                VisiblePasswordTb.Visibility = Visibility.Visible;
            }
            if (string.IsNullOrWhiteSpace(AuthLoginTb.Text) |
                string.IsNullOrWhiteSpace(VisibleAuthPasswordTb.Text))
                LoginBtn.IsEnabled = false;
            else
                LoginBtn.IsEnabled = true;
        }
    }
}
