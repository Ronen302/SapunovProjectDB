using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SapunovProjectDB.Windows.AddEditWindows
{
    public partial class ServiceAddEdit : Window
    {
        private readonly Service _currentService = new Service();
        private readonly Service _newService = new Service();
        public ServiceAddEdit(Service selectedService)
        {
            InitializeComponent();
            if (selectedService != null)
            {
                _currentService = selectedService;
                CancelChangesButton.Visibility = Visibility.Visible;
                ServiceNameTextBox.Text = selectedService.NameService;
                ServicePriceTextBox.Text = selectedService.PriceOfService.ToString();
                ServiceCategoryComboBox.SelectedItem = selectedService.CategoryOfService;
                ServiceDescriptionTextBox.Text = selectedService.Description;
            }
            else
                _currentService = null;
            DataContext = _currentService;
            ServiceCategoryComboBox.ItemsSource = DBEntities.GetContext().CategoryOfService.ToList();
        }
        private void MainBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void userSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentService == null)
            {
                if (DBEntities.GetContext().Service.
                    FirstOrDefault(u => u.NameService == ServiceNameTextBox.Text) != null)
                {
                    ValidationErrorMsg.Text = "Такая услуга уже существует";
                    ValidationErrorMsg.Visibility = Visibility.Visible;
                }
                else
                {
                    try
                    {
                        _newService.NameService = ServiceNameTextBox.Text;
                        _newService.PriceOfService = Decimal.Parse(ServicePriceTextBox.Text.Replace(".", ","));
                        _newService.IdCategory = Int32.Parse(ServiceCategoryComboBox.SelectedValue.ToString());
                        if (!string.IsNullOrWhiteSpace(ServiceDescriptionTextBox.Text))
                            _newService.Description = ServiceDescriptionTextBox.Text;
                        else
                            _newService.Description = null;
                        DBEntities.GetContext().Service.Add(_newService);
                        DBEntities.GetContext().SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Error.ErrorMB(ex);
                    }
                    DialogResult = true;
                }
            }
            else
            {
                try
                {
                    _currentService.NameService = ServiceNameTextBox.Text;
                    _currentService.PriceOfService = Decimal.Parse(ServicePriceTextBox.Text.Replace(".", ","));
                    _currentService.IdCategory = Int32.Parse(ServiceCategoryComboBox.SelectedValue.ToString());
                    if (!string.IsNullOrWhiteSpace(ServiceDescriptionTextBox.Text))
                        _currentService.Description = ServiceDescriptionTextBox.Text;
                    else
                        _currentService.Description = null;
                    DBEntities.GetContext().SaveChanges();
                }
                catch (Exception ex)
                {
                    Error.ErrorMB(ex);
                }
                DialogResult = true;
            }
        }

        private void ServiceNameTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ServiceNameTextBox.Text) |
                string.IsNullOrWhiteSpace(ServicePriceTextBox.Text) |
                ServiceCategoryComboBox.SelectedValue == null)
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void ServicePriceTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ServiceNameTextBox.Text) |
                string.IsNullOrWhiteSpace(ServicePriceTextBox.Text) |
                ServiceCategoryComboBox.SelectedValue == null)
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void ServiceCategoryComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ServiceNameTextBox.Text) |
                string.IsNullOrWhiteSpace(ServicePriceTextBox.Text) |
                ServiceCategoryComboBox.SelectedValue == null)
                userSaveButton.IsEnabled = false;
            else
                userSaveButton.IsEnabled = true;
        }

        private void CancelChangesButton_Click(object sender, RoutedEventArgs e)
        {
            ServiceNameTextBox.Text = _currentService.NameService;
            ServicePriceTextBox.Text = _currentService.PriceOfService.ToString();
            ServiceCategoryComboBox.SelectedItem = _currentService.CategoryOfService;
            ServiceDescriptionTextBox.Text = _currentService.Description;
        }
    }
}
