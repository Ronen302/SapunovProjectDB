using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                    .OrderByDescending(u => u.DateOfCreate).ThenBy(u => u.IdStatusOrder);
            }
            catch (Exception ex)
            {
                Error.ErrorMB(ex);
            }
        }
    }
}
