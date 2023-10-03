﻿using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SapunovProjectDB.Pages.AdminMain
{
    public partial class UserAdd : Page
    {
        public UserAdd()
        {
            InitializeComponent();
            UserRoleComboBox.ItemsSource = DBEntities.GetContext().Role.ToList();
        }

        private void userSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserLoginTextBox.Text) &&
                string.IsNullOrWhiteSpace(UserPasswordTextBox.Text) &&
                UserRoleComboBox.SelectedItem == null)
            {
                EmptyLoginErrorAddUser.Visibility = Visibility.Visible;
                EmptyPasswordErrorAddUser.Visibility = Visibility.Visible;
                EmptyRoleErrorAddUser.Visibility = Visibility.Visible;
            }
            else if(string.IsNullOrWhiteSpace(UserLoginTextBox.Text))
            {
                EmptyLoginErrorAddUser.Visibility = Visibility.Visible;
            }
            else if (string.IsNullOrWhiteSpace(UserPasswordTextBox.Text))
            {
                EmptyPasswordErrorAddUser.Visibility = Visibility.Visible;
            }
            else if (UserRoleComboBox.SelectedItem == null)
            {
                EmptyRoleErrorAddUser.Visibility = Visibility.Visible;
            }
            else
            {
                try
                {
                    var addNewUser = new User()
                    {
                        LoginUser = UserLoginTextBox.Text,
                        PasswordUser = UserPasswordTextBox.Text,
                        IdRole = Int32.Parse(UserRoleComboBox.SelectedValue.ToString())
                    };
                    DBEntities.GetContext().User.Add(addNewUser);
                    DBEntities.GetContext().SaveChanges();
                    NavigationService.Navigate(new UserList());
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

        private void UserLoginTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EmptyLoginErrorAddUser.Visibility = Visibility.Collapsed;
        }

        private void UserPasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EmptyPasswordErrorAddUser.Visibility = Visibility.Collapsed;
        }

        private void UserRoleComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EmptyRoleErrorAddUser.Visibility = Visibility.Collapsed;
        }
    }
}
