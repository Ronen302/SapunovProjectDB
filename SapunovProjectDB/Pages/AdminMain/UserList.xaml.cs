using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SapunovProjectDB.Pages.AdminMain
{
    /// <summary>
    /// Логика взаимодействия для UserList.xaml
    /// </summary>
    public partial class UserList : Page
    {
        public UserList()
        {
            InitializeComponent();
            var allRoleComboBox = DBEntities.GetContext().Role.ToList();

            allRoleComboBox.Insert(0, new Role
            {
                NameRole = "Все"
            });
            FilterRoleCb.ItemsSource = allRoleComboBox;
            FilterRoleCb.SelectedIndex = 0;
            UpdateFilter();
        }

        public void UpdateFilter()
        {
            try
            {
                var currentUser = DBEntities.GetContext().User.ToList();

                if (FilterRoleCb.SelectedIndex > 0)
                {
                    currentUser = currentUser.Where(u => u.IdRole.ToString()
                    .Contains(FilterRoleCb.SelectedIndex.ToString())).ToList();
                }
                UserListDataGrid.ItemsSource = currentUser.OrderBy(u => u.IdUser);
            }
            catch (Exception ex)
            {
                Error.ErrorMB(ex);
            }
        }

        private void UserAddBtn_Click(object sender, RoutedEventArgs e)
        {
            UserListFrame.Navigate(new UserAdd());
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = UserListDataGrid.SelectedItem as User;

                DBEntities.GetContext().User
                    .Remove(user);
                DBEntities.GetContext().SaveChanges();
                UpdateFilter();
            }
            catch (Exception ex)
            {
                Error.ErrorMB(ex);
            }
        }

        private void FilterRoleCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilter();
        }
    }
}
