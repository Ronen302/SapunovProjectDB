using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SapunovProjectDB.Windows.AddEditWindows
{
    public partial class StaffAddEdit : Window
    {
        Staff _currentStaff = new Staff();
        PassportStaff _currentPassportStaff = new PassportStaff();
        AdressStaff _currentAdressStaff = new AdressStaff();
        PassportClient _currentPassportClient = new PassportClient();
        AdressClient _currentAdressClient = new AdressClient();
        Client _currentClient = new Client();
        User _currentUser = new User();
        Staff _newStaff = new Staff();
        PassportStaff _newPassportStaff = new PassportStaff();
        AdressStaff _newAdressStaff = new AdressStaff();
        User _newUser = new User();
        Client _newClient = new Client();
        PassportClient _newPassportClient = new PassportClient();
        AdressClient _newAdressClient = new AdressClient();
        public StaffAddEdit(Staff selectedStaff)
        {
            InitializeComponent();
            User user = DBEntities.GetContext().User
                .FirstOrDefault(u => u.LoginUser == Properties.Settings.Default.CurrentUser);
            if (user.IdRole == 2)
                StaffPasswordPanel.Visibility = Visibility.Collapsed;
            if (selectedStaff != null)
            {
                _currentStaff = selectedStaff;
                _currentPassportStaff.IdPassportStaff = _currentStaff.IdPassportStaff;
                _currentAdressStaff.IdAdressStaff = _currentStaff.IdAdressStaff;
                _currentUser.IdUser = _currentStaff.IdUser;
                CancelChangesButton.Visibility = Visibility.Visible;
                StaffLastNameTextBox.Text = selectedStaff.LastNameStaff;
                StaffFirstNameTextBox.Text = selectedStaff.FirstNameStaff;
                StaffMiddleNameTextBox.Text = selectedStaff.MiddleNameStaff;
                GenderStaffComboBox.SelectedItem = selectedStaff.GenderStaff;
                EducationStaffComboBox.SelectedItem = selectedStaff.Education;
                StaffDateOfBirthDatePicker.SelectedDate = selectedStaff.DateOfBirthStaff;
                StaffPhoneNumberTextBox.Text = selectedStaff.PhoneNumberStaff;
                StaffEmailTextBox.Text = selectedStaff.EmailStaff;
                PositionAtWorkStaffComboBox.SelectedItem = selectedStaff.PositionAtWork;
                StaffSalaryTextBox.Text = selectedStaff.SalaryStaff.ToString();
                StaffHireDateDatePicker.SelectedDate = selectedStaff.HireDateStaff;
                StaffLoginTextBox.Text = selectedStaff.User.LoginUser;
                StaffPasswordTextBox.Text = selectedStaff.User.PasswordUser;
                RoleStaffComboBox.SelectedItem = selectedStaff.User.Role;
                StaffSerialPassportTextBox.Text = selectedStaff.PassportStaff.SerialPassportStaff.ToString();
                StaffNumberPassportTextBox.Text = selectedStaff.PassportStaff.NumberPassportStaff.ToString();
                StaffPassportIssuedByTextBox.Text = selectedStaff.PassportStaff.PassportIssuedByStaff;
                StaffPassportIssueDateDatePicker.SelectedDate = selectedStaff.PassportStaff.PassportIssueDateStaff;
                StaffDepartmentCodeTextBox.Text = selectedStaff.PassportStaff.DepartmentCodeStaff.ToString();
                StaffCityNameTextBox.Text = selectedStaff.AdressStaff.CityNameStaff;
                StaffStreetNameTextBox.Text = selectedStaff.AdressStaff.StreetNameStaff;
                StaffHouseNumberTextBox.Text = selectedStaff.AdressStaff.HouseNumberStaff.ToString();
                StaffApartmentNumberTextBox.Text = selectedStaff.AdressStaff.ApartmentNumberStaff.ToString();
            }
            else
                _currentStaff = null;
            DataContext = _currentStaff;
            GenderStaffComboBox.ItemsSource = DBEntities.GetContext().GenderStaff.ToList();
            EducationStaffComboBox.ItemsSource = DBEntities.GetContext().Education.ToList();
            PositionAtWorkStaffComboBox.ItemsSource = DBEntities.GetContext().PositionAtWork.ToList();
            var role = DBEntities.GetContext().Role.ToList();
            role.RemoveAt(3);
            RoleStaffComboBox.ItemsSource = role;
        }

        private void userSaveButton_Click(object sender, RoutedEventArgs e)
        {
            string upperCaseLetters = "QWERTYUIOPASDFGHJKLZXCVBNMЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ",
                lowerCaseLetters = "qwertyuiopasdfghjklzxcvbnmйцукенгшщзхъфывапролджэячсмитьбю",
                numbers = "1234567890",
                enteredPassword = StaffPasswordTextBox.Text;
            if (_currentStaff == null)
            {
                if (DBEntities.GetContext().User.FirstOrDefault(u => u.LoginUser == 
                StaffLoginTextBox.Text) != null)
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
                        _newPassportStaff.SerialPassportStaff = Int32.Parse(StaffSerialPassportTextBox.Text);
                        _newPassportStaff.NumberPassportStaff = Int32.Parse(StaffNumberPassportTextBox.Text);
                        _newPassportStaff.PassportIssueDateStaff = DateTime.Parse(StaffPassportIssueDateDatePicker.SelectedDate.ToString());
                        _newPassportStaff.PassportIssuedByStaff = StaffPassportIssuedByTextBox.Text;
                        _newPassportStaff.DepartmentCodeStaff = Int32.Parse(StaffDepartmentCodeTextBox.Text);
                        DBEntities.GetContext().PassportStaff.Add(_newPassportStaff);
                        DBEntities.GetContext().SaveChanges();
                        _currentPassportStaff.IdPassportStaff = _newPassportStaff.IdPassportStaff;

                        _newAdressStaff.CityNameStaff = StaffCityNameTextBox.Text;
                        _newAdressStaff.StreetNameStaff = StaffStreetNameTextBox.Text;
                        _newAdressStaff.HouseNumberStaff = Int32.Parse(StaffHouseNumberTextBox.Text);
                        _newAdressStaff.ApartmentNumberStaff = Int32.Parse(StaffApartmentNumberTextBox.Text);
                        DBEntities.GetContext().AdressStaff.Add(_newAdressStaff);
                        DBEntities.GetContext().SaveChanges();
                        _currentAdressStaff.IdAdressStaff = _newAdressStaff.IdAdressStaff;

                        _newUser.LoginUser = StaffLoginTextBox.Text;
                        _newUser.PasswordUser = StaffPasswordTextBox.Text;
                        _newUser.IdRole = Int32.Parse(RoleStaffComboBox.SelectedValue.ToString());
                        DBEntities.GetContext().User.Add(_newUser);
                        DBEntities.GetContext().SaveChanges();
                        _currentUser.IdUser = _newUser.IdUser;

                        _newStaff.FirstNameStaff = StaffFirstNameTextBox.Text;
                        _newStaff.LastNameStaff = StaffLastNameTextBox.Text;
                        if (!string.IsNullOrWhiteSpace(StaffMiddleNameTextBox.Text))
                            _newStaff.MiddleNameStaff = StaffMiddleNameTextBox.Text;
                        else
                        {
                            _newStaff.MiddleNameStaff = null;
                        }
                        _newStaff.IdGenderStaff = Int32.Parse(GenderStaffComboBox.SelectedValue.ToString());
                        _newStaff.IdEducation = Int32.Parse(EducationStaffComboBox.SelectedValue.ToString());
                        _newStaff.DateOfBirthStaff = DateTime.Parse(StaffDateOfBirthDatePicker.SelectedDate.ToString());
                        _newStaff.PhoneNumberStaff = StaffPhoneNumberTextBox.Text;
                        _newStaff.EmailStaff = StaffEmailTextBox.Text;
                        _newStaff.IdPositionAtWork = Int32.Parse(PositionAtWorkStaffComboBox.SelectedValue.ToString());
                        _newStaff.SalaryStaff = Decimal.Parse(StaffSalaryTextBox.Text.Replace(".", ","));
                        _newStaff.HireDateStaff = DateTime.Parse(StaffHireDateDatePicker.SelectedDate.ToString());
                        _newStaff.IdPassportStaff = _currentPassportStaff.IdPassportStaff;
                        _newStaff.IdAdressStaff = _currentAdressStaff.IdAdressStaff;
                        _newStaff.IdUser = _currentUser.IdUser;
                        DBEntities.GetContext().Staff.Add(_newStaff);
                        DBEntities.GetContext().SaveChanges();

                        _newPassportClient.SerialPassportClient = Int32.Parse(StaffSerialPassportTextBox.Text);
                        _newPassportClient.NumberPassportClient = Int32.Parse(StaffNumberPassportTextBox.Text);
                        _newPassportClient.PassportIssueDateClient = DateTime.Parse(StaffPassportIssueDateDatePicker.SelectedDate.ToString());
                        _newPassportClient.PassportIssuedByClient = StaffPassportIssuedByTextBox.Text;
                        _newPassportClient.DepartmentCodeClient = Int32.Parse(StaffDepartmentCodeTextBox.Text);
                        DBEntities.GetContext().PassportClient.Add(_newPassportClient);
                        DBEntities.GetContext().SaveChanges();
                        _currentPassportClient.IdPassportClient = _newPassportClient.IdPassportClient;

                        _newAdressClient.CityNameClient = StaffCityNameTextBox.Text;
                        _newAdressClient.StreetNameClient = StaffStreetNameTextBox.Text;
                        _newAdressClient.HouseNumberClient = Int32.Parse(StaffHouseNumberTextBox.Text);
                        _newAdressClient.ApartmentNumberClient = Int32.Parse(StaffApartmentNumberTextBox.Text);
                        DBEntities.GetContext().AdressClient.Add(_newAdressClient);
                        DBEntities.GetContext().SaveChanges();
                        _currentAdressClient.IdAdressClient = _newAdressClient.IdAdressClient;

                        _newClient.LastNameClient = StaffLastNameTextBox.Text;
                        _newClient.NameClient = StaffFirstNameTextBox.Text;
                        _newClient.MiddleNameClient = StaffMiddleNameTextBox.Text;
                        _newClient.PhoneNumberClient = StaffPhoneNumberTextBox.Text;
                        _newClient.EmailClient = StaffEmailTextBox.Text;
                        _newClient.DateOfRegistration = DateTime.Now;
                        _newClient.IdPassportClient = _currentPassportClient.IdPassportClient;
                        _newClient.IdAdressClient = _currentAdressClient.IdAdressClient;
                        _newClient.IdUser = _currentUser.IdUser;
                        DBEntities.GetContext().Client.Add(_newClient);
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
                    if (_currentStaff.IdUser == DBEntities.GetContext().User
                        .FirstOrDefault(u => u.LoginUser == Properties.Settings.Default.CurrentUser).IdUser)
                    {
                        _currentStaff = DBEntities.GetContext().Staff.FirstOrDefault(u => u.IdStaff == _currentStaff.IdStaff);
                        _currentPassportStaff = DBEntities.GetContext().PassportStaff.FirstOrDefault(u => u.IdPassportStaff == _currentStaff.IdPassportStaff);
                        _currentAdressStaff = DBEntities.GetContext().AdressStaff.FirstOrDefault(u => u.IdAdressStaff == _currentStaff.IdAdressStaff);
                        _currentUser = DBEntities.GetContext().User.FirstOrDefault(u => u.IdUser == _currentStaff.IdUser);
                        _currentClient = DBEntities.GetContext().Client.FirstOrDefault(u => u.IdClient == _currentStaff.IdUser);
                        _currentPassportClient = DBEntities.GetContext().PassportClient.FirstOrDefault(u => u.IdPassportClient == _currentClient.IdPassportClient);
                        _currentAdressClient = DBEntities.GetContext().AdressClient.FirstOrDefault(u => u.IdAdressClient == _currentClient.IdAdressClient);
                        _currentStaff.FirstNameStaff = StaffFirstNameTextBox.Text;
                        _currentStaff.LastNameStaff = StaffLastNameTextBox.Text;
                        if (!string.IsNullOrWhiteSpace(StaffMiddleNameTextBox.Text))
                            _currentStaff.MiddleNameStaff = StaffMiddleNameTextBox.Text;
                        else
                        {
                            _currentStaff.MiddleNameStaff = null;
                        }
                        _currentStaff.IdGenderStaff = Int32.Parse(GenderStaffComboBox.SelectedValue.ToString());
                        _currentStaff.IdEducation = Int32.Parse(EducationStaffComboBox.SelectedValue.ToString());
                        _currentStaff.DateOfBirthStaff = DateTime.Parse(StaffDateOfBirthDatePicker.SelectedDate.ToString());
                        _currentStaff.PhoneNumberStaff = StaffPhoneNumberTextBox.Text;
                        _currentStaff.EmailStaff = StaffEmailTextBox.Text;
                        _currentStaff.IdPositionAtWork = Int32.Parse(PositionAtWorkStaffComboBox.SelectedValue.ToString());
                        _currentStaff.SalaryStaff = Decimal.Parse(StaffSalaryTextBox.Text.Replace(".", ","));
                        _currentStaff.HireDateStaff = DateTime.Parse(StaffHireDateDatePicker.SelectedDate.ToString());

                        _currentPassportStaff.SerialPassportStaff = Int32.Parse(StaffSerialPassportTextBox.Text);
                        _currentPassportStaff.NumberPassportStaff = Int32.Parse(StaffNumberPassportTextBox.Text);
                        _currentPassportStaff.PassportIssueDateStaff = DateTime.Parse(StaffPassportIssueDateDatePicker.SelectedDate.ToString());
                        _currentPassportStaff.PassportIssuedByStaff = StaffPassportIssuedByTextBox.Text;
                        _currentPassportStaff.DepartmentCodeStaff = Int32.Parse(StaffDepartmentCodeTextBox.Text);

                        _currentAdressStaff.CityNameStaff = StaffCityNameTextBox.Text;
                        _currentAdressStaff.StreetNameStaff = StaffStreetNameTextBox.Text;
                        _currentAdressStaff.HouseNumberStaff = Int32.Parse(StaffHouseNumberTextBox.Text);
                        _currentAdressStaff.ApartmentNumberStaff = Int32.Parse(StaffApartmentNumberTextBox.Text);

                        _currentPassportClient.SerialPassportClient = Int32.Parse(StaffSerialPassportTextBox.Text);
                        _currentPassportClient.NumberPassportClient = Int32.Parse(StaffNumberPassportTextBox.Text);
                        _currentPassportClient.PassportIssueDateClient = DateTime.Parse(StaffPassportIssueDateDatePicker.SelectedDate.ToString());
                        _currentPassportClient.PassportIssuedByClient = StaffPassportIssuedByTextBox.Text;
                        _currentPassportClient.DepartmentCodeClient = Int32.Parse(StaffDepartmentCodeTextBox.Text);

                        _currentAdressClient.CityNameClient = StaffCityNameTextBox.Text;
                        _currentAdressClient.StreetNameClient = StaffStreetNameTextBox.Text;
                        _currentAdressClient.HouseNumberClient = Int32.Parse(StaffHouseNumberTextBox.Text);
                        _currentAdressClient.ApartmentNumberClient = Int32.Parse(StaffApartmentNumberTextBox.Text);

                        _currentClient.LastNameClient = StaffLastNameTextBox.Text;
                        _currentClient.NameClient = StaffFirstNameTextBox.Text;
                        _currentClient.MiddleNameClient = StaffMiddleNameTextBox.Text;
                        _currentClient.PhoneNumberClient = StaffPhoneNumberTextBox.Text;
                        _currentClient.EmailClient = StaffEmailTextBox.Text;

                        _currentUser.LoginUser = StaffLoginTextBox.Text;
                        _currentUser.PasswordUser = StaffPasswordTextBox.Text;
                        _currentUser.IdRole = Int32.Parse(RoleStaffComboBox.SelectedValue.ToString());
                        DBEntities.GetContext().SaveChanges();
                        Properties.Settings.Default.CurrentUser = StaffLoginTextBox.Text;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        _currentStaff = DBEntities.GetContext().Staff.FirstOrDefault(u => u.IdStaff == _currentStaff.IdStaff);
                        _currentPassportStaff = DBEntities.GetContext().PassportStaff.FirstOrDefault(u => u.IdPassportStaff == _currentStaff.IdPassportStaff);
                        _currentAdressStaff = DBEntities.GetContext().AdressStaff.FirstOrDefault(u => u.IdAdressStaff == _currentStaff.IdAdressStaff);
                        _currentUser = DBEntities.GetContext().User.FirstOrDefault(u => u.IdUser == _currentStaff.IdUser);
                        _currentClient = DBEntities.GetContext().Client.FirstOrDefault(u => u.IdClient == _currentStaff.IdUser);
                        _currentPassportClient = DBEntities.GetContext().PassportClient.FirstOrDefault(u => u.IdPassportClient == _currentClient.IdPassportClient);
                        _currentAdressClient = DBEntities.GetContext().AdressClient.FirstOrDefault(u => u.IdAdressClient == _currentClient.IdAdressClient);
                        _currentStaff.FirstNameStaff = StaffFirstNameTextBox.Text;
                        _currentStaff.LastNameStaff = StaffLastNameTextBox.Text;
                        if (!string.IsNullOrWhiteSpace(StaffMiddleNameTextBox.Text))
                            _currentStaff.MiddleNameStaff = StaffMiddleNameTextBox.Text;
                        else
                        {
                            _currentStaff.MiddleNameStaff = null;
                        }
                        _currentStaff.IdGenderStaff = Int32.Parse(GenderStaffComboBox.SelectedValue.ToString());
                        _currentStaff.IdEducation = Int32.Parse(EducationStaffComboBox.SelectedValue.ToString());
                        _currentStaff.DateOfBirthStaff = DateTime.Parse(StaffDateOfBirthDatePicker.SelectedDate.ToString());
                        _currentStaff.PhoneNumberStaff = StaffPhoneNumberTextBox.Text;
                        _currentStaff.EmailStaff = StaffEmailTextBox.Text;
                        _currentStaff.IdPositionAtWork = Int32.Parse(PositionAtWorkStaffComboBox.SelectedValue.ToString());
                        _currentStaff.SalaryStaff = Decimal.Parse(StaffSalaryTextBox.Text.Replace(".", ","));
                        _currentStaff.HireDateStaff = DateTime.Parse(StaffHireDateDatePicker.SelectedDate.ToString());

                        _currentPassportStaff.SerialPassportStaff = Int32.Parse(StaffSerialPassportTextBox.Text);
                        _currentPassportStaff.NumberPassportStaff = Int32.Parse(StaffNumberPassportTextBox.Text);
                        _currentPassportStaff.PassportIssueDateStaff = DateTime.Parse(StaffPassportIssueDateDatePicker.SelectedDate.ToString());
                        _currentPassportStaff.PassportIssuedByStaff = StaffPassportIssuedByTextBox.Text;
                        _currentPassportStaff.DepartmentCodeStaff = Int32.Parse(StaffDepartmentCodeTextBox.Text);

                        _currentAdressStaff.CityNameStaff = StaffCityNameTextBox.Text;
                        _currentAdressStaff.StreetNameStaff = StaffStreetNameTextBox.Text;
                        _currentAdressStaff.HouseNumberStaff = Int32.Parse(StaffHouseNumberTextBox.Text);
                        _currentAdressStaff.ApartmentNumberStaff = Int32.Parse(StaffApartmentNumberTextBox.Text);

                        _currentPassportClient.SerialPassportClient = Int32.Parse(StaffSerialPassportTextBox.Text);
                        _currentPassportClient.NumberPassportClient = Int32.Parse(StaffNumberPassportTextBox.Text);
                        _currentPassportClient.PassportIssueDateClient = DateTime.Parse(StaffPassportIssueDateDatePicker.SelectedDate.ToString());
                        _currentPassportClient.PassportIssuedByClient = StaffPassportIssuedByTextBox.Text;
                        _currentPassportClient.DepartmentCodeClient = Int32.Parse(StaffDepartmentCodeTextBox.Text);

                        _currentAdressClient.CityNameClient = StaffCityNameTextBox.Text;
                        _currentAdressClient.StreetNameClient = StaffStreetNameTextBox.Text;
                        _currentAdressClient.HouseNumberClient = Int32.Parse(StaffHouseNumberTextBox.Text);
                        _currentAdressClient.ApartmentNumberClient = Int32.Parse(StaffApartmentNumberTextBox.Text);

                        _currentClient.LastNameClient = StaffLastNameTextBox.Text;
                        _currentClient.NameClient = StaffFirstNameTextBox.Text;
                        _currentClient.MiddleNameClient = StaffMiddleNameTextBox.Text;
                        _currentClient.PhoneNumberClient = StaffPhoneNumberTextBox.Text;
                        _currentClient.EmailClient = StaffEmailTextBox.Text;

                        _currentUser.LoginUser = StaffLoginTextBox.Text;
                        _currentUser.PasswordUser = StaffPasswordTextBox.Text;
                        _currentUser.IdRole = Int32.Parse(RoleStaffComboBox.SelectedValue.ToString());
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

        private void MainBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void StaffLastNameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffFirstNameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void GenderStaffComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void EducationStaffComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffDateOfBirthDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffPhoneNumberTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffEmailTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void PositionAtWorkStaffComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffSalaryTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffHireDateDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffLoginTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffPasswordTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void RoleStaffComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffSerialPassportTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffPassportIssuedByTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffPassportIssueDateDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffDepartmentCodeTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffCityNameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffStreetNameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffHouseNumberTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffApartmentNumberTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffNumberPassportTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text) |
                GenderStaffComboBox.SelectedValue == null |
                EducationStaffComboBox.SelectedValue == null |
                StaffDateOfBirthDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffPhoneNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffEmailTextBox.Text) |
                PositionAtWorkStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text) |
                StaffHireDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffLoginTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text) |
                RoleStaffComboBox.SelectedValue == null |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void CancelChangesButton_Click(object sender, RoutedEventArgs e)
        {
            StaffLastNameTextBox.Text = _currentStaff.LastNameStaff;
            StaffFirstNameTextBox.Text = _currentStaff.FirstNameStaff;
            StaffMiddleNameTextBox.Text = _currentStaff.MiddleNameStaff;
            GenderStaffComboBox.SelectedItem = _currentStaff.GenderStaff;
            EducationStaffComboBox.SelectedItem = _currentStaff.Education;
            StaffDateOfBirthDatePicker.SelectedDate = _currentStaff.DateOfBirthStaff;
            StaffPhoneNumberTextBox.Text = _currentStaff.PhoneNumberStaff;
            StaffEmailTextBox.Text = _currentStaff.EmailStaff;
            PositionAtWorkStaffComboBox.SelectedItem = _currentStaff.PositionAtWork;
            StaffSalaryTextBox.Text = _currentStaff.SalaryStaff.ToString();
            StaffHireDateDatePicker.SelectedDate = _currentStaff.HireDateStaff;
            StaffLoginTextBox.Text = _currentStaff.User.LoginUser;
            StaffPasswordTextBox.Text = _currentStaff.User.PasswordUser;
            RoleStaffComboBox.SelectedItem = _currentStaff.User.Role;
            StaffSerialPassportTextBox.Text = _currentStaff.PassportStaff.SerialPassportStaff.ToString();
            StaffNumberPassportTextBox.Text = _currentStaff.PassportStaff.NumberPassportStaff.ToString();
            StaffPassportIssuedByTextBox.Text = _currentStaff.PassportStaff.PassportIssuedByStaff;
            StaffPassportIssueDateDatePicker.SelectedDate = _currentStaff.PassportStaff.PassportIssueDateStaff;
            StaffDepartmentCodeTextBox.Text = _currentStaff.PassportStaff.DepartmentCodeStaff.ToString();
            StaffCityNameTextBox.Text = _currentStaff.AdressStaff.CityNameStaff;
            StaffStreetNameTextBox.Text = _currentStaff.AdressStaff.StreetNameStaff;
            StaffHouseNumberTextBox.Text = _currentStaff.AdressStaff.HouseNumberStaff.ToString();
            StaffApartmentNumberTextBox.Text = _currentStaff.AdressStaff.ApartmentNumberStaff.ToString();
        }
    }
}
