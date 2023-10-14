using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SapunovProjectDB.Windows.AdminMain
{
    public partial class ServiceAddEdit : Window
    {
        private readonly Service _currentService = new Service();
        public ServiceAddEdit(Service selectedService)
        {
            InitializeComponent();
            if (selectedService != null)
                _currentService = selectedService;
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
                if (string.IsNullOrWhiteSpace(ServiceNameTextBox.Text))
                {
                    ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                    ValidationErrorMsg.Visibility = Visibility.Visible;
                }
                else if (string.IsNullOrWhiteSpace(ServicePriceTextBox.Text))
                {
                    ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                    ValidationErrorMsg.Visibility = Visibility.Visible;
                }
                else if (ServiceCategoryComboBox.SelectedItem == null)
                {
                    ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                    ValidationErrorMsg.Visibility = Visibility.Visible;
                }
                else if (DBEntities.GetContext().Service.
                    FirstOrDefault(u => u.NameService == ServiceNameTextBox.Text) != null)
                {
                    ValidationErrorMsg.Text = "Такая услуга уже существует";
                    ValidationErrorMsg.Visibility = Visibility.Visible;
                }
                else
                {
                    try
                    {
                        var newService = new Service()
                        {
                            NameService = ServiceNameTextBox.Text,
                            PriceOfService = Decimal.Parse(ServicePriceTextBox.Text.Replace(".", ",")),
                            IdCategory = Int32.Parse(ServiceCategoryComboBox.SelectedValue.ToString()),
                            Description = ServiceDescriptionTextBox.Text
                        };
                        DBEntities.GetContext().Service.Add(newService);
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
                if (string.IsNullOrWhiteSpace(ServiceNameTextBox.Text))
                {
                    ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                    ValidationErrorMsg.Visibility = Visibility.Visible;
                }
                else if (string.IsNullOrWhiteSpace(ServicePriceTextBox.Text))
                {
                    ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                    ValidationErrorMsg.Visibility = Visibility.Visible;
                }
                else if (ServiceCategoryComboBox.SelectedItem == null)
                {
                    ValidationErrorMsg.Text = "Заполните все поля, отмеченные *";
                    ValidationErrorMsg.Visibility = Visibility.Visible;
                }
                else
                {
                    try
                    {
                        _currentService.NameService = ServiceNameTextBox.Text;
                        _currentService.PriceOfService = Decimal.Parse(ServicePriceTextBox.Text.Replace(".", ","));
                        _currentService.IdCategory = Int32.Parse(ServiceCategoryComboBox.SelectedValue.ToString());
                        _currentService.Description = ServiceDescriptionTextBox.Text;
                        DBEntities.GetContext().SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Error.ErrorMB(ex);
                    }
                    DialogResult = true;
                }
            }
        }
    }
}
