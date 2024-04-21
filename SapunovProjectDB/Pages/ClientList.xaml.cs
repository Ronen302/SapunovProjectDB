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
            PassportClient passportClient = DBEntities.GetContext().PassportClient
                .FirstOrDefault(u => u.IdPassportClient == client.IdPassportClient);
            AdressClient adressClient = DBEntities.GetContext().AdressClient
                .FirstOrDefault(u => u.IdAdressClient == client.IdAdressClient);
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
                    if (staff != null)
                    {
                        DBEntities.GetContext().Client.Remove(client);
                        DBEntities.GetContext().SaveChanges();
                        DBEntities.GetContext().PassportClient.Remove(passportClient);
                        DBEntities.GetContext().SaveChanges();
                        DBEntities.GetContext().AdressClient.Remove(adressClient);
                        DBEntities.GetContext().SaveChanges();
                        DBEntities.GetContext().Staff.Remove(staff);
                        DBEntities.GetContext().SaveChanges();
                        DBEntities.GetContext().User.Remove(user);
                        DBEntities.GetContext().SaveChanges();
                    }
                    else if (passportClient == null)
                    {
                        DBEntities.GetContext().Client.Remove(client);
                        DBEntities.GetContext().SaveChanges();
                        DBEntities.GetContext().User.Remove(user);
                        DBEntities.GetContext().SaveChanges();
                    }
                    else
                    {
                        DBEntities.GetContext().Client.Remove(client);
                        DBEntities.GetContext().SaveChanges();
                        DBEntities.GetContext().PassportClient.Remove(passportClient);
                        DBEntities.GetContext().SaveChanges();
                        DBEntities.GetContext().AdressClient.Remove(adressClient);
                        DBEntities.GetContext().SaveChanges();
                        DBEntities.GetContext().User.Remove(user);
                        DBEntities.GetContext().SaveChanges();
                    }
                    dataIsSavedMessage.Text = "Данные удалены";
                    UpdateFilter();
                    DataIsSaved();
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
                .StartsWith(FilterTextBox.Text) || u.LastNameClient
                .StartsWith(FilterTextBox.Text) || u.MiddleNameClient
                .StartsWith(FilterTextBox.Text)).ToList();
                ClientListDataGrid.ItemsSource = currentClient.OrderByDescending(u => u.IdClient);
            }
            catch (Exception ex)
            {
                Error.ErrorMB(ex);
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            ClientEdit clientEdit = new ClientEdit(ClientListDataGrid.SelectedItem as Client);
            if (clientEdit.ShowDialog() == true)
            {
                dataIsSavedMessage.Text = "Данные сохранены";
                UpdateFilter();
                DataIsSaved();
            }
        }
        private async void DataIsSaved()
        {
            dataIsSavedMessage.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(2.6));
            dataIsSavedMessage.Visibility = Visibility.Collapsed;
        }
    }
}
