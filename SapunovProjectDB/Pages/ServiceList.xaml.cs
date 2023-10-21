using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using SapunovProjectDB.Windows;
using SapunovProjectDB.Windows.AddEditWindows;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SapunovProjectDB.Pages
{
    public partial class ServiceList : Page
    {
        public ServiceList()
        {
            InitializeComponent();
            var allCategoriesComboBox = DBEntities.GetContext().CategoryOfService.ToList();

            allCategoriesComboBox.Insert(0, new CategoryOfService
            {
                NameCategory = "Все"
            });
            FilterRoleCb.ItemsSource = allCategoriesComboBox;
            FilterRoleCb.SelectedIndex = 0;
            UpdateFilter();
        }
        private void UpdateFilter()
        {
            try
            {
                var currentService = DBEntities.GetContext().Service.ToList();

                if (FilterRoleCb.SelectedIndex > 0)
                {
                    currentService = currentService.Where(u => u.IdCategory.ToString()
                    .Contains(FilterRoleCb.SelectedIndex.ToString())).ToList();
                }
                currentService = currentService.Where(u => u.NameService
                .StartsWith(textFilter.Text)).ToList();
                TypeOfServiceListView.ItemsSource = currentService.OrderBy(u => u.IdService);
            }
            catch (Exception ex)
            {
                Error.ErrorMB(ex);
            }
        }

        private void UserAddBtn_Click(object sender, RoutedEventArgs e)
        {
            ServiceAddEdit serviceAdd = new ServiceAddEdit(null);
            if (serviceAdd.ShowDialog() == true)
            {
                UpdateFilter();
                DataIsSaved();
            }
        }

        private void FilterRoleCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilter();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            ServiceAddEdit serviceEdit = new ServiceAddEdit((sender as Button).DataContext as Service);
            if (serviceEdit.ShowDialog() == true)
            {
                UpdateFilter();
                DataIsSaved();
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Service service = TypeOfServiceListView.SelectedItem as Service;
            var typeOfWorks = DBEntities.GetContext().TypeOfWork
                .Where(u => u.IdService == Properties.Settings.Default.SelectedIdService).ToList();
            RemoveDialogWindow removeDialog = new RemoveDialogWindow();
            removeDialog.removeMessage.Text = $"Услуга будет удалена без возможности восстановления." +
                        $" Вы действительно желаете это сделать?";
            if (removeDialog.ShowDialog() == true)
            {
                try
                {
                    DBEntities.GetContext().TypeOfWork.RemoveRange(typeOfWorks);
                    DBEntities.GetContext().Service.Remove(service);
                    DBEntities.GetContext().SaveChanges();
                    UpdateFilter();
                }
                catch (Exception ex)
                {
                    Error.ErrorMB(ex);
                }
            }
        }
        public void openServiceButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedIdService = (int)(sender as Button).Tag;
            Properties.Settings.Default.SelectedIdService = selectedIdService;
            Properties.Settings.Default.Save();
            NavigationService.Navigate(new TypeOfWorkList());
        }

        private void textFilter_TextChanged(object sender, TextChangedEventArgs e)
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
