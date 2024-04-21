using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SapunovProjectDB.Windows.AddEditWindows
{
    public partial class ClientEdit : Window
    {
        Client _currentClient = new Client();
        PassportClient _currentPassportClient = new PassportClient();
        AdressClient _currentAdressClient = new AdressClient();
        User _currentUser = new User();
        Staff _currentStaff = new Staff();
        PassportStaff _currentPassportStaff = new PassportStaff();
        AdressStaff _currentAdressStaff = new AdressStaff();
        PassportClient _newPassportClient = new PassportClient();
        AdressClient _newAdressClient = new AdressClient();
        public ClientEdit(Client selectedClient)
        {
            InitializeComponent();
            _currentClient = selectedClient;
            if (_currentClient.IdPassportClient != null)
            {
                _currentClient = selectedClient;
                _currentPassportClient.IdPassportClient = (int)_currentClient.IdPassportClient;
                _currentAdressClient.IdAdressClient = (int)_currentClient.IdAdressClient;
                ClientLastNameTextBox.Text = selectedClient.LastNameClient;
                ClientNameTextBox.Text = selectedClient.NameClient;
                ClientMiddleNameTextBox.Text = selectedClient.MiddleNameClient;
                StaffSerialPassportTextBox.Text = selectedClient.PassportClient.SerialPassportClient.ToString();
                StaffNumberPassportTextBox.Text = selectedClient.PassportClient.NumberPassportClient.ToString();
                StaffPassportIssuedByTextBox.Text = selectedClient.PassportClient.PassportIssuedByClient;
                StaffPassportIssueDateDatePicker.SelectedDate = selectedClient.PassportClient.PassportIssueDateClient;
                StaffDepartmentCodeTextBox.Text = selectedClient.PassportClient.DepartmentCodeClient.ToString();
                StaffCityNameTextBox.Text = selectedClient.AdressClient.CityNameClient;
                StaffStreetNameTextBox.Text = selectedClient.AdressClient.StreetNameClient;
                StaffHouseNumberTextBox.Text = selectedClient.AdressClient.HouseNumberClient.ToString();
                StaffApartmentNumberTextBox.Text = selectedClient.AdressClient.ApartmentNumberClient.ToString();
            }
            else
            {
                ClientLastNameTextBox.Text = null;
                ClientNameTextBox.Text = selectedClient.NameClient;
                ClientMiddleNameTextBox.Text = null;
                StaffSerialPassportTextBox.Text = null;
                StaffNumberPassportTextBox.Text = null;
                StaffPassportIssuedByTextBox.Text = null;
                StaffPassportIssueDateDatePicker.SelectedDate = null;
                StaffDepartmentCodeTextBox.Text = null;
                StaffCityNameTextBox.Text = null;
                StaffStreetNameTextBox.Text = null;
                StaffHouseNumberTextBox.Text = null;
                StaffApartmentNumberTextBox.Text = null;
            }
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
                        _currentClient = DBEntities.GetContext().Client.FirstOrDefault(u => u.IdClient == _currentClient.IdClient);

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
                        _currentClient.NameClient = ClientNameTextBox.Text;
                        if (!string.IsNullOrWhiteSpace(ClientMiddleNameTextBox.Text))
                            _currentClient.MiddleNameClient = ClientMiddleNameTextBox.Text;
                        else
                        {
                            _currentClient.MiddleNameClient = null;
                        }
                        _currentClient.IdPassportClient = _currentPassportClient.IdPassportClient;
                        _currentClient.IdAdressClient = _currentAdressClient.IdAdressClient;
                        DBEntities.GetContext().SaveChanges();
                    }
                    else
                    {
                        _currentStaff = DBEntities.GetContext().Staff.FirstOrDefault(u => u.IdUser == _currentClient.IdUser);
                        if (_currentStaff != null)
                        {
                            _currentPassportClient = DBEntities.GetContext().PassportClient.FirstOrDefault(u => u.IdPassportClient == _currentClient.IdPassportClient);
                            _currentAdressClient = DBEntities.GetContext().AdressClient.FirstOrDefault(u => u.IdAdressClient == _currentClient.IdAdressClient);
                            _currentPassportStaff = DBEntities.GetContext().PassportStaff.FirstOrDefault(u => u.IdPassportStaff == _currentStaff.IdPassportStaff);
                            _currentAdressStaff = DBEntities.GetContext().AdressStaff.FirstOrDefault(u => u.IdAdressStaff == _currentStaff.IdAdressStaff);

                            _currentPassportClient.SerialPassportClient = Int32.Parse(StaffSerialPassportTextBox.Text);
                            _currentPassportClient.NumberPassportClient = Int32.Parse(StaffNumberPassportTextBox.Text);
                            _currentPassportClient.PassportIssueDateClient = DateTime.Parse(StaffPassportIssueDateDatePicker.SelectedDate.ToString());
                            _currentPassportClient.PassportIssuedByClient = StaffPassportIssuedByTextBox.Text;
                            _currentPassportClient.DepartmentCodeClient = Int32.Parse(StaffDepartmentCodeTextBox.Text);

                            _currentAdressClient.CityNameClient = StaffCityNameTextBox.Text;
                            _currentAdressClient.StreetNameClient = StaffStreetNameTextBox.Text;
                            _currentAdressClient.HouseNumberClient = Int32.Parse(StaffHouseNumberTextBox.Text);
                            _currentAdressClient.ApartmentNumberClient = Int32.Parse(StaffApartmentNumberTextBox.Text);

                            _currentClient.LastNameClient = ClientLastNameTextBox.Text;
                            _currentClient.NameClient = ClientNameTextBox.Text;
                            if (!string.IsNullOrWhiteSpace(ClientMiddleNameTextBox.Text))
                                _currentClient.MiddleNameClient = ClientMiddleNameTextBox.Text;
                            else
                            {
                                _currentClient.MiddleNameClient = null;
                            }

                            _currentStaff.FirstNameStaff = ClientNameTextBox.Text;
                            _currentStaff.LastNameStaff = ClientLastNameTextBox.Text;
                            if (!string.IsNullOrWhiteSpace(ClientMiddleNameTextBox.Text))
                                _currentStaff.MiddleNameStaff = ClientMiddleNameTextBox.Text;
                            else
                            {
                                _currentStaff.MiddleNameStaff = null;
                            }

                            _currentPassportStaff.SerialPassportStaff = Int32.Parse(StaffSerialPassportTextBox.Text);
                            _currentPassportStaff.NumberPassportStaff = Int32.Parse(StaffNumberPassportTextBox.Text);
                            _currentPassportStaff.PassportIssueDateStaff = DateTime.Parse(StaffPassportIssueDateDatePicker.SelectedDate.ToString());
                            _currentPassportStaff.PassportIssuedByStaff = StaffPassportIssuedByTextBox.Text;
                            _currentPassportStaff.DepartmentCodeStaff = Int32.Parse(StaffDepartmentCodeTextBox.Text);

                            _currentAdressStaff.CityNameStaff = StaffCityNameTextBox.Text;
                            _currentAdressStaff.StreetNameStaff = StaffStreetNameTextBox.Text;
                            _currentAdressStaff.HouseNumberStaff = Int32.Parse(StaffHouseNumberTextBox.Text);
                            _currentAdressStaff.ApartmentNumberStaff = Int32.Parse(StaffApartmentNumberTextBox.Text);
                            DBEntities.GetContext().SaveChanges();
                        }
                        else
                        {
                            _currentClient = DBEntities.GetContext().Client.FirstOrDefault(u => u.IdClient == _currentClient.IdClient);
                            _currentPassportClient = DBEntities.GetContext().PassportClient.FirstOrDefault(u => u.IdPassportClient == _currentClient.IdPassportClient);
                            _currentAdressClient = DBEntities.GetContext().AdressClient.FirstOrDefault(u => u.IdAdressClient == _currentClient.IdAdressClient);

                            _currentClient.LastNameClient = ClientLastNameTextBox.Text;
                            _currentClient.NameClient = ClientNameTextBox.Text;
                            if (!string.IsNullOrWhiteSpace(ClientMiddleNameTextBox.Text))
                                _currentClient.MiddleNameClient = ClientMiddleNameTextBox.Text;
                            else
                            {
                                _currentClient.MiddleNameClient = null;
                            }

                            _currentPassportClient.SerialPassportClient = Int32.Parse(StaffSerialPassportTextBox.Text);
                            _currentPassportClient.NumberPassportClient = Int32.Parse(StaffNumberPassportTextBox.Text);
                            _currentPassportClient.PassportIssueDateClient = DateTime.Parse(StaffPassportIssueDateDatePicker.SelectedDate.ToString());
                            _currentPassportClient.PassportIssuedByClient = StaffPassportIssuedByTextBox.Text;
                            _currentPassportClient.DepartmentCodeClient = Int32.Parse(StaffDepartmentCodeTextBox.Text);

                            _currentAdressClient.CityNameClient = StaffCityNameTextBox.Text;
                            _currentAdressClient.StreetNameClient = StaffStreetNameTextBox.Text;
                            _currentAdressClient.HouseNumberClient = Int32.Parse(StaffHouseNumberTextBox.Text);
                            _currentAdressClient.ApartmentNumberClient = Int32.Parse(StaffApartmentNumberTextBox.Text);
                            DBEntities.GetContext().SaveChanges();
                        }
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

        private void ClientLastNameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClientLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(ClientNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void ClientNameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClientLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(ClientNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffSerialPassportTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClientLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(ClientNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffNumberPassportTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClientLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(ClientNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffPassportIssuedByTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClientLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(ClientNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffPassportIssueDateDatePicker_SelectedDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClientLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(ClientNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffDepartmentCodeTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClientLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(ClientNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffCityNameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClientLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(ClientNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffStreetNameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClientLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(ClientNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffHouseNumberTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClientLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(ClientNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void StaffApartmentNumberTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClientLastNameTextBox.Text) |
                string.IsNullOrWhiteSpace(ClientNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffSerialPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffNumberPassportTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffPassportIssuedByTextBox.Text) |
                StaffPassportIssueDateDatePicker.SelectedDate == null |
                string.IsNullOrWhiteSpace(StaffDepartmentCodeTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffCityNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffStreetNameTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffHouseNumberTextBox.Text) |
                string.IsNullOrWhiteSpace(StaffApartmentNumberTextBox.Text))
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void CancelChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentClient.IdPassportClient != null)
            {
                ClientLastNameTextBox.Text = _currentClient.LastNameClient;
                ClientNameTextBox.Text = _currentClient.NameClient;
                ClientMiddleNameTextBox.Text = _currentClient.MiddleNameClient;
                StaffSerialPassportTextBox.Text = _currentClient.PassportClient.SerialPassportClient.ToString();
                StaffNumberPassportTextBox.Text = _currentClient.PassportClient.NumberPassportClient.ToString();
                StaffPassportIssuedByTextBox.Text = _currentClient.PassportClient.PassportIssuedByClient;
                StaffPassportIssueDateDatePicker.SelectedDate = _currentClient.PassportClient.PassportIssueDateClient;
                StaffDepartmentCodeTextBox.Text = _currentClient.PassportClient.DepartmentCodeClient.ToString();
                StaffCityNameTextBox.Text = _currentClient.AdressClient.CityNameClient;
                StaffStreetNameTextBox.Text = _currentClient.AdressClient.StreetNameClient;
                StaffHouseNumberTextBox.Text = _currentClient.AdressClient.HouseNumberClient.ToString();
                StaffApartmentNumberTextBox.Text = _currentClient.AdressClient.ApartmentNumberClient.ToString();
            }
            else
            {
                ClientLastNameTextBox.Text = null;
                ClientNameTextBox.Text = _currentClient.NameClient;
                ClientMiddleNameTextBox.Text = null;
                StaffSerialPassportTextBox.Text = null;
                StaffNumberPassportTextBox.Text = null;
                StaffPassportIssuedByTextBox.Text = null;
                StaffPassportIssueDateDatePicker.SelectedDate = null;
                StaffDepartmentCodeTextBox.Text = null;
                StaffCityNameTextBox.Text = null;
                StaffStreetNameTextBox.Text = null;
                StaffHouseNumberTextBox.Text = null;
                StaffApartmentNumberTextBox.Text = null;
            }
        }
    }
}
