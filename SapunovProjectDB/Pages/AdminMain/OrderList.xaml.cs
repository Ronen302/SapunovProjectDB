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

namespace SapunovProjectDB.Pages.AdminMain
{
    public partial class OrderList : Page
    {
        public OrderList()
        {
            InitializeComponent();
            OrderListDataGrid.ItemsSource = DBEntities.GetContext().Order.ToList()
                .OrderBy(u => u.DateOfCreate);
        }

        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void FilterStatusCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
