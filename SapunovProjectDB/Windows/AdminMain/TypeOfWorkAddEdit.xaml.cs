using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SapunovProjectDB.Windows.AdminMain
{
    public partial class TypeOfWorkAddEdit : Window
    {
        private readonly TypeOfWork _currentTypeOfWork = new TypeOfWork();
        private readonly TypeOfWork _newTypeOfWork = new TypeOfWork();
        public TypeOfWorkAddEdit(TypeOfWork selectedTypeOfWork)
        {
            InitializeComponent();
            if (selectedTypeOfWork != null)
                _currentTypeOfWork = selectedTypeOfWork;
            else
                _currentTypeOfWork = null;
            DataContext = _currentTypeOfWork;
        }
        private void MainBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void userSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentTypeOfWork == null)
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
                else if (DBEntities.GetContext().TypeOfWork.
                    FirstOrDefault(u => u.NameTypeOfWork == ServiceNameTextBox.Text) != null)
                {
                    ValidationErrorMsg.Text = "Такая услуга уже существует";
                    ValidationErrorMsg.Visibility = Visibility.Visible;
                }
                else
                {
                    try
                    {
                        _newTypeOfWork.NameTypeOfWork = ServiceNameTextBox.Text;
                        _newTypeOfWork.PriceOfWork = Decimal.Parse(ServicePriceTextBox.Text.Replace(".", ","));
                        _newTypeOfWork.IdService = Properties.Settings.Default.SelectedIdService;
                        if (string.IsNullOrEmpty(ServiceDescriptionTextBox.Text))
                            _newTypeOfWork.DescriptionOfWork = null;
                        else
                            _newTypeOfWork.DescriptionOfWork = ServiceDescriptionTextBox.Text;
                        DBEntities.GetContext().TypeOfWork.Add(_newTypeOfWork);
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
                else
                {
                    try
                    {
                        _currentTypeOfWork.NameTypeOfWork = ServiceNameTextBox.Text;
                        _currentTypeOfWork.PriceOfWork = Decimal.Parse(ServicePriceTextBox.Text.Replace(".", ","));
                        _newTypeOfWork.IdService = Properties.Settings.Default.SelectedIdService;
                        if (string.IsNullOrEmpty(ServiceDescriptionTextBox.Text))
                            _currentTypeOfWork.DescriptionOfWork = null;
                        else
                            _currentTypeOfWork.DescriptionOfWork = ServiceDescriptionTextBox.Text;
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
