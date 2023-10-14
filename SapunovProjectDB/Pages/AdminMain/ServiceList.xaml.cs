using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using SapunovProjectDB.Windows.AdminMain;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SapunovProjectDB.Pages.AdminMain
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
                UpdateFilter();
        }

        private void FilterRoleCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilter();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            ServiceAddEdit serviceAdd = new ServiceAddEdit((sender as Button).DataContext as Service);
            if (serviceAdd.ShowDialog() == true)
                UpdateFilter();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
