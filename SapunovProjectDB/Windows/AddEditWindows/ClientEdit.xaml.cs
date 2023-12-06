using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Windows;
using System.Windows.Input;

namespace SapunovProjectDB.Windows.AddEditWindows
{
    public partial class ClientEdit : Window
    {
        private readonly Client _currentClient = new Client();
        private readonly PassportClient _currentPassportClient = new PassportClient();
        private readonly AdressClient _currentAdressClient = new AdressClient();
        private readonly PassportClient _newPassportClient = new PassportClient();
        private readonly AdressClient _newAdressClient = new AdressClient();
        public ClientEdit(Client selectedClient)
        {
            InitializeComponent();
            if (selectedClient != null)
            {
                _currentClient = selectedClient;
                if(_currentClient.IdPassportClient != null)
                {
                    _currentPassportClient.IdPassportClient = (int)_currentClient.IdPassportClient;
                    _currentAdressClient.IdAdressClient = (int)_currentClient.IdAdressClient;
                }
            }
            else
                _currentClient = null;
            DataContext = _currentClient;
        }

        private void userSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) ||
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) ||
                StaffPassportIssueDateDatePicker.SelectedDate == null ||
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) ||
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) ||
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) ||
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
            {
                ValidationErrorMsg.Text = "Заполните обязательные поля";
                ValidationErrorMsg.Visibility = Visibility.Visible;
            }
            else
            {
                try
                {
                    if (_currentClient.IdPassportClient == null)
                    {
                        _newPassportClient.SerialPassportClient = Int32.Parse(StaffSerialPassportTextBox.Text);
                        _newPassportClient.NumberPassportClient = Int32.Parse(StaffNumberPassportTextBox.Text);
                        _newPassportClient.PassportIssueDateClient = DateTime.Parse(StaffPassportIssueDateDatePicker.SelectedDate.ToString());
                        _newPassportClient.PassportIssuedByClient = StaffPassportIssuedByTextBox.Text;
                        _newPassportClient.DepartmentCodeClient = Int32.Parse(StaffDepartmentCodeTextBox.Text);
                        DBEntities.GetContext().PassportClient.Add(_newPassportClient);
                        DBEntities.GetContext().SaveChanges();
                        _currentPassportClient.IdPassportClient = _newPassportClient.IdPassportClient;

                        _newAdressClient.CityNameClient = StaffCityNameTextBox.Text;
                        _newAdressClient.StreetNameClient = StaffStreetNameTextBox.Text;
                        _newAdressClient.HouseNumberClient = Int32.Parse(StaffHouseNumberTextBox.Text);
                        _newAdressClient.ApartmentNumberClient = Int32.Parse(StaffApartmentNumberTextBox.Text);
                        DBEntities.GetContext().AdressClient.Add(_newAdressClient);
                        DBEntities.GetContext().SaveChanges();
                        _currentAdressClient.IdAdressClient = _newAdressClient.IdAdressClient;

                        _currentClient.LastNameClient = ClientLastNameTextBox.Text;
                        _currentClient.MiddleNameClient = ClientMiddleNameTextBox.Text;
                        _currentClient.IdPassportClient = _currentPassportClient.IdPassportClient;
                        _currentClient.IdAdressClient = _currentAdressClient.IdAdressClient;
                        DBEntities.GetContext().SaveChanges();
                    }
                    else
                    {
                        _currentPassportClient.SerialPassportClient = Int32.Parse(StaffSerialPassportTextBox.Text);
                        _currentPassportClient.NumberPassportClient = Int32.Parse(StaffNumberPassportTextBox.Text);
                        _currentPassportClient.PassportIssueDateClient = DateTime.Parse(StaffPassportIssueDateDatePicker.SelectedDate.ToString());
                        _currentPassportClient.PassportIssuedByClient = StaffPassportIssuedByTextBox.Text;
                        _currentPassportClient.DepartmentCodeClient = Int32.Parse(StaffDepartmentCodeTextBox.Text);
                        DBEntities.GetContext().SaveChanges();

                        _currentAdressClient.CityNameClient = StaffCityNameTextBox.Text;
                        _currentAdressClient.StreetNameClient = StaffStreetNameTextBox.Text;
                        _currentAdressClient.HouseNumberClient = Int32.Parse(StaffHouseNumberTextBox.Text);
                        _currentAdressClient.ApartmentNumberClient = Int32.Parse(StaffApartmentNumberTextBox.Text);
                        DBEntities.GetContext().SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Error.ErrorMB(ex);
                }
                DialogResult = true;
            }
        }

        private void MainBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
