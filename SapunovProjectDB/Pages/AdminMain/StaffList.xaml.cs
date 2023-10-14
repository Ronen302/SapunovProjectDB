using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using SapunovProjectDB.Windows;
using SapunovProjectDB.Windows.AdminMain;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SapunovProjectDB.Pages.AdminMain
{
    public partial class StaffList : Page
    {
        public StaffList()
        {
            InitializeComponent();
            var allEducationComboBox = DBEntities.GetContext().Education.ToList();
            allEducationComboBox.Insert(0, new Education
            {
                NameEducation = "Все"
            });
            FilterEducationCb.ItemsSource = allEducationComboBox;
            FilterEducationCb.SelectedIndex = 0;

            var allRoleComboBox = DBEntities.GetContext().Role.ToList();
            allRoleComboBox.Insert(0, new Role
            {
                NameRole = "Все"
            });
            FilterRoleCb.ItemsSource = allRoleComboBox;
            FilterRoleCb.SelectedIndex = 0;
            UpdateFilter();
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateFilter();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            StaffAddEdit staffEdit = new StaffAddEdit(StaffListDataGrid.SelectedItem as Staff);
            if (staffEdit.ShowDialog() == true)
                UpdateFilter();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Staff staff = StaffListDataGrid.SelectedItem as Staff;
            User user = DBEntities.GetContext().User
                        .FirstOrDefault(u => u.IdUser == staff.IdUser);
            RemoveDialogWindow removeDialog = new RemoveDialogWindow();
            removeDialog.removeMessage.Text = $"\"{staff.LastNameStaff} {staff.FirstNameStaff} {staff.MiddleNameStaff}\" будет удален без возможности восстановления." +
                        $" Вы действительно желаете это сделать?";
            if (removeDialog.ShowDialog() == true)
            {
                try
                {
                    DBEntities.GetContext().Staff.Remove(staff);
                    DBEntities.GetContext().User.Remove(user);
                    DBEntities.GetContext().SaveChanges();
                    UpdateFilter();
                }
                catch (Exception ex)
                {
                    Error.ErrorMB(ex);
                }
            }
        }

        private void UserAddBtn_Click(object sender, RoutedEventArgs e)
        {
            StaffAddEdit staffEdit = new StaffAddEdit(null);
            if (staffEdit.ShowDialog() == true)
                UpdateFilter();
        }

        private void FilterRoleCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilter();
        }
        private void FilterEducationCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilter();
        }
        private void UpdateFilter()
        {
            try
            {
                var currentStaff = DBEntities.GetContext().Staff.ToList();

                if (FilterEducationCb.SelectedIndex > 0)
                {
                    currentStaff = currentStaff.Where(u => u.IdEducation.ToString()
                    .Contains(FilterEducationCb.SelectedIndex.ToString())).ToList();
                }
                if (FilterRoleCb.SelectedIndex > 0)
                {
                    currentStaff = currentStaff.Where(u => u.User.IdRole.ToString()
                    .Contains(FilterRoleCb.SelectedIndex.ToString())).ToList();
                }
                currentStaff = currentStaff.Where(u => u.FirstNameStaff
                .StartsWith(FilterTextBox.Text) || u.LastNameStaff
                .StartsWith(FilterTextBox.Text) || u.MiddleNameStaff
                .StartsWith(FilterTextBox.Text)).ToList();
                StaffListDataGrid.ItemsSource = currentStaff.OrderBy(u => u.IdStaff);
            }
            catch (Exception ex)
            {
                Error.ErrorMB(ex);
            }
        }
    }
}
