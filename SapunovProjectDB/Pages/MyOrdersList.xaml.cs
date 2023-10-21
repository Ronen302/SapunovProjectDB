using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Linq;
using System.Windows.Controls;

namespace SapunovProjectDB.Pages
{
    public partial class MyOrdersList : Page
    {
        public MyOrdersList()
        {
            InitializeComponent();
            var allStatusComboBox = DBEntities.GetContext().StatusOrder.ToList();

            allStatusComboBox.Insert(0, new StatusOrder
            {
                NameStatusOrder = "Все"
            });
            FilterStatusCb.ItemsSource = allStatusComboBox;
            FilterStatusCb.SelectedIndex = 0;
            UpdateFilter();
        }

        private void FilterStatusCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFilter();
        }
        private void UpdateFilter()
        {
            try
            {
                int _currentIdUser = DBEntities.GetContext().User
                    .FirstOrDefault(u => u.LoginUser == Properties.Settings.Default.CurrentUser).IdUser;
                int _currentIdClient = DBEntities.GetContext().Client
                    .FirstOrDefault(u => u.IdUser == _currentIdUser).IdClient;
                var currentOrder = DBEntities.GetContext().Order
                    .Where(u => u.IdClient == _currentIdClient).ToList();

                if (FilterStatusCb.SelectedIndex > 0)
                {
                    currentOrder = currentOrder.Where(u => u.IdStatusOrder.ToString()
                    .Contains(FilterStatusCb.SelectedIndex.ToString())).ToList();
                }
                MyOrdersListView.ItemsSource = currentOrder.OrderByDescending(u => u.DateOfCreate);
            }
            catch (Exception ex)
            {
                Error.ErrorMB(ex);
            }
        }
    }
}
