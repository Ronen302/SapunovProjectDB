﻿using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SapunovProjectDB.Pages
{
    public partial class AccountSettings : Page
    {
        User _currentUser = DBEntities.GetContext().User
                .FirstOrDefault(u => u.LoginUser == Properties.Settings.Default.CurrentUser);
        public AccountSettings()
        {
            InitializeComponent();
            UserLoginTextBox.Text = Properties.Settings.Default.CurrentUser;
            UserIdRun.Text = _currentUser.IdUser.ToString();
            Staff staff = DBEntities.GetContext().Staff.FirstOrDefault(u => u.IdUser == _currentUser.IdUser);
            Client client = DBEntities.GetContext().Client.FirstOrDefault(u => u.IdUser == _currentUser.IdUser);
            if(staff != null)
            {
                UserLastNameRun.Text = staff.LastNameStaff + " ";
                UserFirstNameRun.Text = staff.FirstNameStaff;
                UserMiddleNameRun.Text = staff.MiddleNameStaff;
                SerialPassportRun.Text = staff.PassportStaff.SerialPassportStaff.ToString();
                NumberPassportRun.Text = staff.PassportStaff.NumberPassportStaff.ToString();
                PassportIssuedByTb.Text = staff.PassportStaff.PassportIssuedByStaff;
                PassportIssuedDateTb.Text = staff.PassportStaff.PassportIssueDateStaff.ToString("dd/MM/yyyy").Replace(".", "/");
                PassportDepartmentCodeTb.Text = staff.PassportStaff.DepartmentCodeStaff.ToString("###-###");
                UserLastNameTextBox.Text = staff.LastNameStaff;
                UserFirstNameTextBox.Text = staff.FirstNameStaff;
                UserMiddleNameTextBox.Text = staff.MiddleNameStaff;
                UserPhoneNumberTextBox.Text = staff.PhoneNumberStaff;
                UserEmailTextBox.Text = staff.EmailStaff;
            }
            else
            {
                if (client.PassportClient != null)
                {
                    UserLastNameRun.Text = client.LastNameClient + " ";
                    UserFirstNameRun.Text = client.NameClient;
                    UserMiddleNameRun.Text = client.MiddleNameClient;
                    SerialPassportRun.Text = client.PassportClient.SerialPassportClient.ToString();
                    NumberPassportRun.Text = client.PassportClient.NumberPassportClient.ToString();
                    PassportIssuedByTb.Text = client.PassportClient.PassportIssuedByClient;
                    PassportIssuedDateTb.Text = client.PassportClient.PassportIssueDateClient.ToString("dd/MM/yyyy").Replace(".", "/");
                    PassportDepartmentCodeTb.Text = client.PassportClient.DepartmentCodeClient.ToString("###-###");
                    UserLastNameTextBox.Text = client.LastNameClient;
                    UserFirstNameTextBox.Text = client.NameClient;
                    UserMiddleNameTextBox.Text = client.MiddleNameClient;
                    UserPhoneNumberTextBox.Text = client.PhoneNumberClient;
                    UserEmailTextBox.Text = client.EmailClient;
                }
                else
                {
                    UserLastNameRun.Text = client.LastNameClient + " ";
                    UserFirstNameRun.Text = client.NameClient;
                    UserMiddleNameRun.Text = client.MiddleNameClient;
                    SerialPassportRun.Text = "Не указано";
                    PassportIssuedByTb.Text = "Не указано";
                    PassportIssuedDateTb.Text = "Не указано";
                    PassportDepartmentCodeTb.Text = "Не указано";
                    UserLastNameTextBox.Text = client.LastNameClient;
                    UserFirstNameTextBox.Text = client.NameClient;
                    UserMiddleNameTextBox.Text = client.MiddleNameClient;
                    UserPhoneNumberTextBox.Text = client.PhoneNumberClient;
                    UserEmailTextBox.Text = client.EmailClient;
                }
            }
            if (client.LastNameClient == null)
                UserLastNameRun.Visibility = Visibility.Collapsed;
        }

        private void userSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserLastNameTextBox.Text))
            {
                InvalidCurrentUserLastNameError.Text = "Поле не может быть пустым";
                InvalidCurrentUserLastNameError.Visibility = Visibility.Visible;
                return;
            }
            else if (string.IsNullOrWhiteSpace(UserFirstNameTextBox.Text))
            {
                InvalidCurrentUserFirstNameError.Text = "Поле не может быть пустым";
                InvalidCurrentUserFirstNameError.Visibility = Visibility.Visible;
                return;
            }
            else if (string.IsNullOrWhiteSpace(UserPhoneNumberTextBox.Text))
            {
                InvalidCurrentUserPhoneNumberError.Text = "Поле не может быть пустым";
                InvalidCurrentUserPhoneNumberError.Visibility = Visibility.Visible;
                return;
            }
            else if (string.IsNullOrWhiteSpace(UserEmailTextBox.Text))
            {
                InvalidCurrentUserEmailError.Text = "Поле не может быть пустым";
                InvalidCurrentUserEmailError.Visibility = Visibility.Visible;
                return;
            }
            else if (string.IsNullOrWhiteSpace(UserLoginTextBox.Text))
            {
                InvalidCurrentLoginError.Text = "Поле не может быть пустым";
                InvalidCurrentLoginError.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                try
                {
                    Staff staff = DBEntities.GetContext().Staff.FirstOrDefault(u => u.IdUser == _currentUser.IdUser);
                    Client client = DBEntities.GetContext().Client.FirstOrDefault(u => u.IdUser == _currentUser.IdUser);
                    if (staff != null & string.IsNullOrWhiteSpace(UserNewPasswordTextBox.Password) &&
                        staff != null & string.IsNullOrWhiteSpace(UserNewVisiblePasswordTextBox.Text))
                    {
                        staff.LastNameStaff = UserLastNameTextBox.Text;
                        staff.FirstNameStaff = UserFirstNameTextBox.Text;
                        if (string.IsNullOrWhiteSpace(UserMiddleNameTextBox.Text))
                            staff.MiddleNameStaff = null;
                        else
                            staff.MiddleNameStaff = UserMiddleNameTextBox.Text;
                        staff.PhoneNumberStaff = UserPhoneNumberTextBox.Text;
                        staff.EmailStaff = UserEmailTextBox.Text;
                        client.LastNameClient = UserLastNameTextBox.Text;
                        client.NameClient = UserFirstNameTextBox.Text;
                        if (string.IsNullOrWhiteSpace(UserMiddleNameTextBox.Text))
                            client.MiddleNameClient = null;
                        else
                            client.MiddleNameClient = UserMiddleNameTextBox.Text;
                        client.PhoneNumberClient = UserPhoneNumberTextBox.Text;
                        client.EmailClient = UserEmailTextBox.Text;
                        if (DBEntities.GetContext().User.FirstOrDefault(u => u.LoginUser == UserLoginTextBox.Text) == null ||
                            _currentUser.LoginUser == UserLoginTextBox.Text)
                        {
                            _currentUser.LoginUser = UserLoginTextBox.Text;
                        }
                        else
                        {
                            InvalidCurrentLoginError.Text = "Такой логин уже существует";
                            InvalidCurrentLoginError.Visibility = Visibility.Visible;
                            return;
                        }
                        DBEntities.GetContext().SaveChanges();
                        Properties.Settings.Default.CurrentUser = UserLoginTextBox.Text;
                        Properties.Settings.Default.Save();
                        DataIsSaved();
                    }
                    else if (staff == null & string.IsNullOrWhiteSpace(UserNewPasswordTextBox.Password) &&
                        staff == null & string.IsNullOrWhiteSpace(UserNewVisiblePasswordTextBox.Text))
                    {
                        client.LastNameClient = UserLastNameTextBox.Text;
                        client.NameClient = UserFirstNameTextBox.Text;
                        if (string.IsNullOrWhiteSpace(UserMiddleNameTextBox.Text))
                            client.MiddleNameClient = null;
                        else
                            client.MiddleNameClient = UserMiddleNameTextBox.Text;
                        client.PhoneNumberClient = UserPhoneNumberTextBox.Text;
                        client.EmailClient = UserEmailTextBox.Text;
                        if (DBEntities.GetContext().User.FirstOrDefault(u => u.LoginUser == UserLoginTextBox.Text) == null ||
                            _currentUser.LoginUser == UserLoginTextBox.Text)
                        {
                            _currentUser.LoginUser = UserLoginTextBox.Text;
                        }
                        else
                        {
                            InvalidCurrentLoginError.Text = "Такой логин уже существует";
                            InvalidCurrentLoginError.Visibility = Visibility.Visible;
                            return;
                        }
                        DBEntities.GetContext().SaveChanges();
                        Properties.Settings.Default.CurrentUser = UserLoginTextBox.Text;
                        Properties.Settings.Default.Save();
                        DataIsSaved();
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(UserNewPasswordTextBox.Password))
                        {
                            if (_currentUser.PasswordUser != UserCurrentPasswordTextBox.Password)
                            {
                                InvalidCurrentPaswwordError.Text = "Неверный пароль";
                                InvalidCurrentPaswwordError.Visibility = Visibility.Visible;
                                return;
                            }
                            else
                            {
                                try
                                {
                                    if (staff != null)
                                    {
                                        staff.LastNameStaff = UserLastNameTextBox.Text;
                                        staff.FirstNameStaff = UserFirstNameTextBox.Text;
                                        if (string.IsNullOrWhiteSpace(UserMiddleNameTextBox.Text))
                                            staff.MiddleNameStaff = null;
                                        else
                                            staff.MiddleNameStaff = UserMiddleNameTextBox.Text;
                                        staff.PhoneNumberStaff = UserPhoneNumberTextBox.Text;
                                        staff.EmailStaff = UserEmailTextBox.Text;
                                        client.LastNameClient = UserLastNameTextBox.Text;
                                        client.NameClient = UserFirstNameTextBox.Text;
                                        if (string.IsNullOrWhiteSpace(UserMiddleNameTextBox.Text))
                                            client.MiddleNameClient = null;
                                        else
                                            client.MiddleNameClient = UserMiddleNameTextBox.Text;
                                        client.PhoneNumberClient = UserPhoneNumberTextBox.Text;
                                        client.EmailClient = UserEmailTextBox.Text;
                                        if (DBEntities.GetContext().User.FirstOrDefault(u => u.LoginUser == UserLoginTextBox.Text) == null ||
                                            _currentUser.LoginUser == UserLoginTextBox.Text)
                                        {
                                            _currentUser.LoginUser = UserLoginTextBox.Text;
                                        }
                                        else
                                        {
                                            InvalidCurrentLoginError.Text = "Такой логин уже существует";
                                            InvalidCurrentLoginError.Visibility = Visibility.Visible;
                                            return;
                                        }
                                        _currentUser.PasswordUser = UserNewPasswordTextBox.Password;
                                        DBEntities.GetContext().SaveChanges();
                                        Properties.Settings.Default.CurrentUser = UserLoginTextBox.Text;
                                        Properties.Settings.Default.Save();
                                        DataIsSaved();
                                    }
                                    else
                                    {
                                        client.LastNameClient = UserLastNameTextBox.Text;
                                        client.NameClient = UserFirstNameTextBox.Text;
                                        if (string.IsNullOrWhiteSpace(UserMiddleNameTextBox.Text))
                                            client.MiddleNameClient = null;
                                        else
                                            client.MiddleNameClient = UserMiddleNameTextBox.Text;
                                        client.PhoneNumberClient = UserPhoneNumberTextBox.Text;
                                        client.EmailClient = UserEmailTextBox.Text;
                                        if (DBEntities.GetContext().User.FirstOrDefault(u => u.LoginUser == UserLoginTextBox.Text) == null ||
                                            _currentUser.LoginUser == UserLoginTextBox.Text)
                                        {
                                            _currentUser.LoginUser = UserLoginTextBox.Text;
                                        }
                                        else
                                        {
                                            InvalidCurrentLoginError.Text = "Такой логин уже существует";
                                            InvalidCurrentLoginError.Visibility = Visibility.Visible;
                                            return;
                                        }
                                        _currentUser.PasswordUser = UserNewPasswordTextBox.Password;
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
                        }
                        else
                        {
                            if (_currentUser.PasswordUser != UserCurrentPasswordTextBox.Password)
                            {
                                InvalidCurrentPaswwordError.Text = "Неверный пароль";
                                InvalidCurrentPaswwordError.Visibility = Visibility.Visible;
                                return;
                            }
                            else
                            {
                                try
                                {
                                    if (staff != null)
                                    {
                                        staff.LastNameStaff = UserLastNameTextBox.Text;
                                        staff.FirstNameStaff = UserFirstNameTextBox.Text;
                                        if (string.IsNullOrWhiteSpace(UserMiddleNameTextBox.Text))
                                            staff.MiddleNameStaff = null;
                                        else
                                            staff.MiddleNameStaff = UserMiddleNameTextBox.Text;
                                        staff.PhoneNumberStaff = UserPhoneNumberTextBox.Text;
                                        staff.EmailStaff = UserEmailTextBox.Text;
                                        client.LastNameClient = UserLastNameTextBox.Text;
                                        client.NameClient = UserFirstNameTextBox.Text;
                                        if (string.IsNullOrWhiteSpace(UserMiddleNameTextBox.Text))
                                            client.MiddleNameClient = null;
                                        else
                                            client.MiddleNameClient = UserMiddleNameTextBox.Text;
                                        client.PhoneNumberClient = UserPhoneNumberTextBox.Text;
                                        client.EmailClient = UserEmailTextBox.Text;
                                        if (DBEntities.GetContext().User.FirstOrDefault(u => u.LoginUser == UserLoginTextBox.Text) == null ||
                                            _currentUser.LoginUser == UserLoginTextBox.Text)
                                        {
                                            _currentUser.LoginUser = UserLoginTextBox.Text;
                                        }
                                        else
                                        {
                                            InvalidCurrentLoginError.Text = "Такой логин уже существует";
                                            InvalidCurrentLoginError.Visibility = Visibility.Visible;
                                            return;
                                        }
                                        _currentUser.PasswordUser = UserNewVisiblePasswordTextBox.Text;
                                        DBEntities.GetContext().SaveChanges();
                                        Properties.Settings.Default.CurrentUser = UserLoginTextBox.Text;
                                        Properties.Settings.Default.Save();
                                        DataIsSaved();
                                    }
                                    else
                                    {
                                        client.LastNameClient = UserLastNameTextBox.Text;
                                        client.NameClient = UserFirstNameTextBox.Text;
                                        if (string.IsNullOrWhiteSpace(UserMiddleNameTextBox.Text))
                                            client.MiddleNameClient = null;
                                        else
                                            client.MiddleNameClient = UserMiddleNameTextBox.Text;
                                        client.PhoneNumberClient = UserPhoneNumberTextBox.Text;
                                        client.EmailClient = UserEmailTextBox.Text;
                                        if (DBEntities.GetContext().User.FirstOrDefault(u => u.LoginUser == UserLoginTextBox.Text) == null ||
                                            _currentUser.LoginUser == UserLoginTextBox.Text)
                                        {
                                            _currentUser.LoginUser = UserLoginTextBox.Text;
                                        }
                                        else
                                        {
                                            InvalidCurrentLoginError.Text = "Такой логин уже существует";
                                            InvalidCurrentLoginError.Visibility = Visibility.Visible;
                                            return;
                                        }
                                        _currentUser.PasswordUser = UserNewVisiblePasswordTextBox.Text;
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
                        }
                    }
                }
                catch (Exception ex)
                {
                    Error.ErrorMB(ex);
                }
            }
        }

        private void UserCurrentPasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InvalidCurrentPaswwordError.Visibility = Visibility.Collapsed;
        }
        private async void DataIsSaved()
        {
            Staff staff = DBEntities.GetContext().Staff.FirstOrDefault(u => u.IdUser == _currentUser.IdUser);
            Client client = DBEntities.GetContext().Client.FirstOrDefault(u => u.IdUser == _currentUser.IdUser);
            if (staff != null)
            {
                UserLastNameRun.Text = staff.LastNameStaff + " ";
                UserFirstNameRun.Text = staff.FirstNameStaff;
                UserMiddleNameRun.Text = staff.MiddleNameStaff;
            }
            else
            {
                UserLastNameRun.Visibility = Visibility.Visible;
                UserLastNameRun.Text = client.LastNameClient + " ";
                UserFirstNameRun.Text = client.NameClient;
                UserMiddleNameRun.Text = client.MiddleNameClient;
            }

            dataIsSavedMessage.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(2.6));
            dataIsSavedMessage.Visibility = Visibility.Collapsed;
        }

        private void UserLastNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InvalidCurrentUserLastNameError.Visibility = Visibility.Collapsed;
        }

        private void UserEmailTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InvalidCurrentUserEmailError.Visibility = Visibility.Collapsed;
        }

        private void UserFirstNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InvalidCurrentUserFirstNameError.Visibility = Visibility.Collapsed;
        }

        private void UserPhoneNumberTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InvalidCurrentUserPhoneNumberError.Visibility = Visibility.Collapsed;
        }

        private void UserLoginTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            InvalidCurrentLoginError.Visibility = Visibility.Collapsed;
        }

        private void VisibilityPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserNewPasswordTextBox.Visibility == Visibility.Visible)
            {
                UserNewVisiblePasswordTextBox.Text = UserNewPasswordTextBox.Password;
                VisiblePasswordIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.EyeOffOutline;
                VisiblePasswordIcon.Margin = new Thickness(0, 1.5, 0, 0);
                UserNewVisiblePasswordTextBox.Visibility = Visibility.Visible;
                UserNewPasswordTextBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                UserNewPasswordTextBox.Password = UserNewVisiblePasswordTextBox.Text;
                VisiblePasswordIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.EyeOutline;
                VisiblePasswordIcon.Margin = new Thickness(0, 0, 0, 0);
                UserNewPasswordTextBox.Visibility = Visibility.Visible;
                UserNewVisiblePasswordTextBox.Visibility = Visibility.Collapsed;
            }
        }

        private void UserNewPasswordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            string upperCaseLetters = "QWERTYUIOPASDFGHJKLZXCVBNMЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ",
                lowerCaseLetters = "qwertyuiopasdfghjklzxcvbnmйцукенгшщзхъфывапролджэячсмитьбю",
                numbers = "1234567890",
                enteredPassword = UserNewPasswordTextBox.Password;

            if (upperCaseLetters.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                InvalidNewPaswwordError.Visibility = Visibility.Visible;
                userSaveButton.IsEnabled = false;
            }
            else if (lowerCaseLetters.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                InvalidNewPaswwordError.Visibility = Visibility.Visible;
                userSaveButton.IsEnabled = false;
            }
            else if (numbers.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                InvalidNewPaswwordError.Visibility = Visibility.Visible;
                userSaveButton.IsEnabled = false;
            }
            else if (enteredPassword.Length < 8)
            {
                InvalidNewPaswwordError.Visibility = Visibility.Visible;
                userSaveButton.IsEnabled = false;
            }
            else
            {
                InvalidNewPaswwordError.Visibility = Visibility.Collapsed;
                userSaveButton.IsEnabled = true;
            }
            if (string.IsNullOrWhiteSpace(UserNewPasswordTextBox.Password))
            {
                userSaveButton.IsEnabled = true;
                InvalidNewPaswwordError.Visibility = Visibility.Collapsed;
            }
        }

        private void UserNewVisiblePasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string upperCaseLetters = "QWERTYUIOPASDFGHJKLZXCVBNMЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ",
                lowerCaseLetters = "qwertyuiopasdfghjklzxcvbnmйцукенгшщзхъфывапролджэячсмитьбю",
                numbers = "1234567890",
                enteredPassword = UserNewVisiblePasswordTextBox.Text;

            if (upperCaseLetters.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                InvalidNewPaswwordError.Visibility = Visibility.Visible;
                userSaveButton.IsEnabled = false;
            }
            else if (lowerCaseLetters.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                InvalidNewPaswwordError.Visibility = Visibility.Visible;
                userSaveButton.IsEnabled = false;
            }
            else if (numbers.IndexOfAny(enteredPassword.ToCharArray()) == -1)
            {
                InvalidNewPaswwordError.Visibility = Visibility.Visible;
                userSaveButton.IsEnabled = false;
            }
            else if (enteredPassword.Length < 8)
            {
                InvalidNewPaswwordError.Visibility = Visibility.Visible;
                userSaveButton.IsEnabled = false;
            }
            else
            {
                InvalidNewPaswwordError.Visibility = Visibility.Collapsed;
                userSaveButton.IsEnabled = true;
            }
            if (string.IsNullOrWhiteSpace(UserNewVisiblePasswordTextBox.Text))
            {
                userSaveButton.IsEnabled = true;
                InvalidNewPaswwordError.Visibility = Visibility.Collapsed;
            }
        }
    }
}
