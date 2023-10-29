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
        private readonly Staff _currentStaff = new Staff();
        private readonly User _currentUser = new User();
        private readonly Staff _newStaff = new Staff();
        private readonly User _newUser = new User();
        private readonly Client _newClient = new Client();
        public StaffAddEdit(Staff selectedStaff)
        {
            InitializeComponent();
            if (selectedStaff != null)
            {
                _currentStaff = selectedStaff;
                _currentUser.IdUser = _currentStaff.IdUser;
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
                        _newStaff.IdGenderStaff = Int32.Parse(GenderStaffComboBox.SelectedValue.ToString());
                        _newStaff.IdEducation = Int32.Parse(EducationStaffComboBox.SelectedValue.ToString());
                        _newStaff.DateOfBirthStaff = DateTime.Parse(StaffDateOfBirthDatePicker.SelectedDate.ToString());
                        _newStaff.PhoneNumberStaff = StaffPhoneNumberTextBox.Text;
                        _newStaff.EmailStaff = StaffEmailTextBox.Text;
                        _newStaff.IdPositionAtWork = Int32.Parse(PositionAtWorkStaffComboBox.SelectedValue.ToString());
                        _newStaff.SalaryStaff = Decimal.Parse(StaffSalaryTextBox.Text.Replace(".", ","));
                        _newStaff.HireDateStaff = DateTime.Parse(StaffHireDateDatePicker.SelectedDate.ToString());
                        _newStaff.IdUser = _currentUser.IdUser;
                        DBEntities.GetContext().Staff.Add(_newStaff);
                        DBEntities.GetContext().SaveChanges();

                        _newClient.NameClient = StaffFirstNameTextBox.Text;
                        _newClient.PhoneNumberClient = StaffPhoneNumberTextBox.Text;
                        _newClient.EmailClient = StaffEmailTextBox.Text;
                        _newClient.DateOfRegistration = DateTime.Now;
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
                    _currentStaff.FirstNameStaff = StaffFirstNameTextBox.Text;
                    _currentStaff.LastNameStaff = StaffLastNameTextBox.Text;
                    if (!string.IsNullOrWhiteSpace(StaffMiddleNameTextBox.Text))
                        _currentStaff.MiddleNameStaff = StaffMiddleNameTextBox.Text;
                    _currentStaff.IdGenderStaff = Int32.Parse(GenderStaffComboBox.SelectedValue.ToString());
                    _currentStaff.IdEducation = Int32.Parse(EducationStaffComboBox.SelectedValue.ToString());
                    _currentStaff.DateOfBirthStaff = DateTime.Parse(StaffDateOfBirthDatePicker.SelectedDate.ToString());
                    _currentStaff.PhoneNumberStaff = StaffPhoneNumberTextBox.Text;
                    _currentStaff.EmailStaff = StaffEmailTextBox.Text;
                    _currentStaff.IdPositionAtWork = Int32.Parse(PositionAtWorkStaffComboBox.SelectedValue.ToString());
                    _currentStaff.SalaryStaff = Decimal.Parse(StaffSalaryTextBox.Text.Replace(".", ","));
                    _currentStaff.HireDateStaff = DateTime.Parse(StaffHireDateDatePicker.SelectedDate.ToString());
                    DBEntities.GetContext().SaveChanges();

                    _currentUser.LoginUser = StaffLoginTextBox.Text;
                    _currentUser.PasswordUser = StaffPasswordTextBox.Text;
                    _currentUser.IdRole = Int32.Parse(RoleStaffComboBox.SelectedValue.ToString());
                    DBEntities.GetContext().SaveChanges();
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
                RoleStaffComboBox.SelectedValue == null)
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
                RoleStaffComboBox.SelectedValue == null)
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
                RoleStaffComboBox.SelectedValue == null)
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
                RoleStaffComboBox.SelectedValue == null)
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
                RoleStaffComboBox.SelectedValue == null)
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
                RoleStaffComboBox.SelectedValue == null)
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
                RoleStaffComboBox.SelectedValue == null)
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
                RoleStaffComboBox.SelectedValue == null)
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
                RoleStaffComboBox.SelectedValue == null)
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
                RoleStaffComboBox.SelectedValue == null)
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
                RoleStaffComboBox.SelectedValue == null)
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
                RoleStaffComboBox.SelectedValue == null)
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
                RoleStaffComboBox.SelectedValue == null)
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }
    }
}
