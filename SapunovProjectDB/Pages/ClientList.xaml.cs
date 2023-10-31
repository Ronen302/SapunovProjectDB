using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using SapunovProjectDB.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SapunovProjectDB.Pages
{
    public partial class ClientList : Page
    {
        public ClientList()
        {
            InitializeComponent();
            UpdateFilter();
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateFilter();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Client client = ClientListDataGrid.SelectedItem as Client;
            User user = DBEntities.GetContext().User
                .FirstOrDefault(u => u.IdUser == client.IdUser);
            Staff staff = DBEntities.GetContext().Staff
                .FirstOrDefault(u => u.IdUser == client.IdUser);
            RemoveDialogWindow removeDialog = new RemoveDialogWindow();
            removeDialog.removeMessage.Text = $"\"{client.NameClient}\" будет удален без возможности восстановления." +
                        $" Вы действительно желаете это сделать?";
            if (removeDialog.ShowDialog() == true)
            {
                try
                {
                    if(DBEntities.GetContext().Staff
                        .FirstOrDefault(u => u.IdUser == staff.IdUser) != null)
                    {
                        DBEntities.GetContext().Staff.Remove(staff);
                        DBEntities.GetContext().Client.Remove(client);
                        DBEntities.GetContext().User.Remove(user);
                        DBEntities.GetContext().SaveChanges();
                    }
                    else
                    {
                        DBEntities.GetContext().Client.Remove(client);
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
        private void UpdateFilter()
        {
            try
            {
                var currentClient = DBEntities.GetContext().Client.ToList();

                currentClient = currentClient.Where(u => u.NameClient
                .StartsWith(FilterTextBox.Text) || u.IdClient.ToString()
                .StartsWith(FilterTextBox.Text)).ToList();
                ClientListDataGrid.ItemsSource = currentClient.OrderByDescending(u => u.IdClient);
            }
            catch (Exception ex)
            {
                Error.ErrorMB(ex);
            }
        }
    }
}
