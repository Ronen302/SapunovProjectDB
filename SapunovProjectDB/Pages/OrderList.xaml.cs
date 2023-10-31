using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SapunovProjectDB.Pages
{
    public partial class OrderList : Page
    {
        public OrderList()
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
                var currentOrder = DBEntities.GetContext().Order.ToList();
                if (FilterStatusCb.SelectedIndex > 0)
                {
                    currentOrder = currentOrder.Where(u => u.IdStatusOrder.ToString()
                    .Contains(FilterStatusCb.SelectedIndex.ToString())).ToList();
                }
                OrderListDataGrid.ItemsSource = currentOrder
                    .OrderByDescending(u => u.DateOfCreate);
            }
            catch (Exception ex)
            {
                Error.ErrorMB(ex);
            }
        }

        private void AcceptMi_Click(object sender, RoutedEventArgs e)
        {
            Order order = OrderListDataGrid.SelectedItem as Order;
            order.IdStatusOrder = 1;
            DBEntities.GetContext().SaveChanges();
            UpdateFilter();
        }

        private void CompleteMi_Click(object sender, RoutedEventArgs e)
        {
            Order order = OrderListDataGrid.SelectedItem as Order;
            order.IdStatusOrder = 2;
            DBEntities.GetContext().SaveChanges();
            UpdateFilter();
        }

        private void CancelMi_Click(object sender, RoutedEventArgs e)
        {
            Order order = OrderListDataGrid.SelectedItem as Order;
            order.IdStatusOrder = 3;
            DBEntities.GetContext().SaveChanges();
            UpdateFilter();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            statusMenu.IsOpen = true;
        }
    }
}
