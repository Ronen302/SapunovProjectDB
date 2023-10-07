﻿using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
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
            StaffListFrame.Navigate(new StaffEdit(StaffListDataGrid.SelectedItem as Staff));
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Staff staffForRemove = StaffListDataGrid.SelectedItem as Staff;
                var userForRemove = DBEntities.GetContext().User.FirstOrDefault(u => u.IdUser == staffForRemove.IdUser);
                DBEntities.GetContext().Staff.Remove(staffForRemove);
                DBEntities.GetContext().SaveChanges();
                DBEntities.GetContext().User.Remove(userForRemove);
                DBEntities.GetContext().SaveChanges();
                UpdateFilter();
            }
            catch (Exception ex)
            {
                Error.ErrorMB(ex);
            }
        }

        private void UserAddBtn_Click(object sender, RoutedEventArgs e)
        {
            StaffListFrame.Navigate(new StaffAdd());
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
