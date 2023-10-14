using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SapunovProjectDB.Pages.AdminMain
{
    public partial class ClientList : Page
    {
        public ClientList()
        {
            InitializeComponent();
            ClientListDataGrid.ItemsSource = DBEntities.GetContext().Client.ToList()
                .OrderBy(u => u.IdClient);
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                ClientListDataGrid.ItemsSource = DBEntities.GetContext().Client
                    .Where(u => u.IdClient.ToString()
                    .StartsWith(FilterTextBox.Text))
                    .ToList().OrderBy(u => u.IdClient);
            }
            catch (Exception ex)
            {
                Error.ErrorMB(ex);
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Client clientForRemove = ClientListDataGrid.SelectedItem as Client;
                var userForRemove = DBEntities.GetContext().User.FirstOrDefault(u => u.IdUser == clientForRemove.IdUser);
                DBEntities.GetContext().Client.Remove(clientForRemove);
                DBEntities.GetContext().SaveChanges();
                DBEntities.GetContext().User.Remove(userForRemove);
                DBEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                Error.ErrorMB(ex);
            }
        }
    }
}
