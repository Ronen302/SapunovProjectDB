using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using SapunovProjectDB.Windows;
using SapunovProjectDB.Windows.AddEditWindows;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SapunovProjectDB.Pages
{
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

        private void UpdateFilter()
        {
            try
            {
                var currentUser = DBEntities.GetContext().User.ToList();

                if (FilterRoleCb.SelectedIndex > 0)
                {
                    currentUser = currentUser.Where(u => u.IdRole.ToString()
                    .Contains(FilterRoleCb.SelectedIndex.ToString())).ToList();
                }
                currentUser = currentUser.Where(u => u.LoginUser
                .StartsWith(FilterTextBox.Text) || u.IdUser.ToString()
                .StartsWith(FilterTextBox.Text)).ToList();
                UserListDataGrid.ItemsSource = currentUser.OrderByDescending(u => u.IdUser);
            }
            catch (Exception ex)
            {
                Error.ErrorMB(ex);
            }
        }

        private void UserAddBtn_Click(object sender, RoutedEventArgs e)
        {
            UserAddEdit userAdd = new UserAddEdit(null);
            if (userAdd.ShowDialog() == true)
            {
                UpdateFilter();
                DataIsSaved();
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            UserAddEdit userEdit = new UserAddEdit(UserListDataGrid.SelectedItem as User);
            if (userEdit.ShowDialog() == true)
            {
                UpdateFilter();
                DataIsSaved();
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            User user = UserListDataGrid.SelectedItem as User;
            Staff staff = DBEntities.GetContext().Staff
                .FirstOrDefault(u => u.IdUser == user.IdUser);
            Client client = DBEntities.GetContext().Client
                .FirstOrDefault(u => u.IdUser == user.IdUser);
            RemoveDialogWindow removeDialog = new RemoveDialogWindow();
            removeDialog.removeMessage.Text = $"\"{user.LoginUser}\" будет удален без возможности восстановления." +
                        $" Вы действительно желаете это сделать?";
            if (removeDialog.ShowDialog() == true)
            {
                try
                {
                    if(DBEntities.GetContext().Staff
                        .FirstOrDefault(u => u.IdUser == user.IdUser) != null)
                    {
                        DBEntities.GetContext().Staff.Remove(staff);
                        DBEntities.GetContext().Client.Remove(client);
                        DBEntities.GetContext().User.Remove(user);
                        DBEntities.GetContext().SaveChanges();
                    }
                    else if(DBEntities.GetContext().Client
                        .FirstOrDefault(u => u.IdUser == user.IdUser) != null)
                    {
                        DBEntities.GetContext().Client.Remove(client);
                        DBEntities.GetContext().User.Remove(user);
                        DBEntities.GetContext().SaveChanges();
                    }
                    else
                    {
                        DBEntities.GetContext().User.Remove(user);
                        DBEntities.GetContext().SaveChanges();
                    }
                    UpdateFilter();
                }
                catch (Exception ex)
                {
                    Error.ErrorMB(ex);
                }
            }
        }

        private void FilterRoleCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilter();
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateFilter();
        }

        private async void DataIsSaved()
        {
            dataIsSavedMessage.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(2.6));
            dataIsSavedMessage.Visibility = Visibility.Collapsed;
        }
    }
}
