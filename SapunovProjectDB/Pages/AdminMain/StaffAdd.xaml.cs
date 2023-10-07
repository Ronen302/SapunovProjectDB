using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SapunovProjectDB.Pages.AdminMain
{
    public partial class StaffAdd : Page
    {
        Staff staff = new Staff();
        User user = new User();
        public StaffAdd()
        {
            InitializeComponent();
            GenderStaffComboBox.ItemsSource = DBEntities.GetContext().GenderStaff.ToList();
            EducationStaffComboBox.ItemsSource = DBEntities.GetContext().Education.ToList();
            PositionAtWorkStaffComboBox.ItemsSource = DBEntities.GetContext().PositionAtWork.ToList();
            RoleStaffComboBox.ItemsSource = DBEntities.GetContext().Role.ToList();
        }

        private void userSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffFirstNameTextBox.Text))
            {
                ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                ValidationErrorMsg.Visibility = Visibility.Visible;
            }
            else if (string.IsNullOrWhiteSpace(StaffLastNameTextBox.Text))
            {
                ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                ValidationErrorMsg.Visibility = Visibility.Visible;
            }
            else if (GenderStaffComboBox.SelectedItem == null)
            {
                ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                ValidationErrorMsg.Visibility = Visibility.Visible;
            }
            else if (EducationStaffComboBox.SelectedItem == null)
            {
                ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                ValidationErrorMsg.Visibility = Visibility.Visible;
            }
            else if (StaffDateOfBirthDatePicker.SelectedDate == null)
            {
                ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                ValidationErrorMsg.Visibility = Visibility.Visible;
            }
            else if (PositionAtWorkStaffComboBox.SelectedItem == null)
            {
                ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                ValidationErrorMsg.Visibility = Visibility.Visible;
            }
            else if (string.IsNullOrWhiteSpace(StaffEmailTextBox.Text))
            {
                ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                ValidationErrorMsg.Visibility = Visibility.Visible;
            }
            else if (string.IsNullOrWhiteSpace(StaffSalaryTextBox.Text))
            {
                ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                ValidationErrorMsg.Visibility = Visibility.Visible;
            }
            else if (StaffHireDateDatePicker.SelectedDate == null)
            {
                ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                ValidationErrorMsg.Visibility = Visibility.Visible;
            }
            else if (string.IsNullOrWhiteSpace(StaffLoginTextBox.Text))
            {
                ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                ValidationErrorMsg.Visibility = Visibility.Visible;
            }
            else if (string.IsNullOrWhiteSpace(StaffPasswordTextBox.Text))
            {
                ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                ValidationErrorMsg.Visibility = Visibility.Visible;
            }
            else if (RoleStaffComboBox.SelectedItem == null)
            {
                ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                ValidationErrorMsg.Visibility = Visibility.Visible;
            }
            else if (DBEntities.GetContext().User.FirstOrDefault(u => u.LoginUser == StaffLoginTextBox.Text) != null)
            {
                ValidationErrorMsg.Text = "Такой логин уже существует";
                ValidationErrorMsg.Visibility = Visibility.Visible;
            }
            else
            {
                try
                {
                    var newUser = new User()
                    {
                        LoginUser = StaffLoginTextBox.Text,
                        PasswordUser = StaffPasswordTextBox.Text,
                        IdRole = Int32.Parse(RoleStaffComboBox.SelectedValue.ToString())
                    };
                    DBEntities.GetContext().User.Add(newUser);
                    DBEntities.GetContext().SaveChanges();
                    user.IdUser = newUser.IdUser;

                    var newStaff = new Staff()
                    {
                        FirstNameStaff = StaffFirstNameTextBox.Text,
                        LastNameStaff = StaffLastNameTextBox.Text,
                        MiddleNameStaff = StaffMiddleNameTextBox.Text,
                        IdGenderStaff = Int32.Parse(GenderStaffComboBox.SelectedValue.ToString()),
                        IdEducation = Int32.Parse(EducationStaffComboBox.SelectedValue.ToString()),
                        DateOfBirthStaff = DateTime.Parse(StaffDateOfBirthDatePicker.SelectedDate.ToString()),
                        PhoneNumberStaff = StaffPhoneNumberTextBox.Text,
                        EmailStaff = StaffEmailTextBox.Text,
                        IdPositionAtWork = Int32.Parse(PositionAtWorkStaffComboBox.SelectedValue.ToString()),
                        SalaryStaff = Decimal.Parse(StaffSalaryTextBox.Text.Replace(".", ",")),
                        HireDateStaff = DateTime.Parse(StaffHireDateDatePicker.SelectedDate.ToString()),
                        IdUser = user.IdUser
                    };
                    DBEntities.GetContext().Staff.Add(newStaff);
                    DBEntities.GetContext().SaveChanges();
                    NavigationService.Navigate(new StaffList());
                }
                catch (Exception ex)
                {
                    Error.ErrorMB(ex);
                }
            }
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Content = null;
        }
    }
}
