using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SapunovProjectDB.Pages.AdminMain
{
    public partial class StaffEdit : Page
    {
        Staff staff = new Staff();
        User user = new User();
        public StaffEdit(Staff staff)
        {
            InitializeComponent();
            DataContext = staff;
            this.staff.IdStaff = staff.IdStaff;
            user.IdUser = (int)staff.IdUser;
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
            else
            {
                try
                {
                    staff = DBEntities.GetContext().Staff.FirstOrDefault(u => u.IdStaff == staff.IdStaff);
                    user = DBEntities.GetContext().User.FirstOrDefault(u => u.IdUser == staff.IdUser);
                    staff.FirstNameStaff = StaffFirstNameTextBox.Text;
                    staff.LastNameStaff = StaffLastNameTextBox.Text;
                    staff.MiddleNameStaff = StaffMiddleNameTextBox.Text;
                    staff.IdGenderStaff = Int32.Parse(GenderStaffComboBox.SelectedValue.ToString());
                    staff.IdEducation = Int32.Parse(EducationStaffComboBox.SelectedValue.ToString());
                    staff.DateOfBirthStaff = DateTime.Parse(StaffDateOfBirthDatePicker.SelectedDate.ToString());
                    staff.PhoneNumberStaff = StaffPhoneNumberTextBox.Text;
                    staff.EmailStaff = StaffEmailTextBox.Text;
                    staff.IdPositionAtWork = Int32.Parse(PositionAtWorkStaffComboBox.SelectedValue.ToString());
                    staff.SalaryStaff = Decimal.Parse(StaffSalaryTextBox.Text.Replace(".", ","));
                    staff.HireDateStaff = DateTime.Parse(StaffHireDateDatePicker.SelectedDate.ToString());
                    DBEntities.GetContext().SaveChanges();

                    user.LoginUser = StaffLoginTextBox.Text;
                    user.PasswordUser = StaffPasswordTextBox.Text;
                    user.IdRole = Int32.Parse(RoleStaffComboBox.SelectedValue.ToString());
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
