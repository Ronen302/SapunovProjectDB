using SapunovProjectDB.Data;
using SapunovProjectDB.Pages.AdminMain.TypeOfWork;
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
    /// <summary>
    /// Логика взаимодействия для TypeOfServiceList.xaml
    /// </summary>
    public partial class TypeOfServiceList : Page
    {
        public TypeOfServiceList()
        {
            InitializeComponent();
            TypeOfServiceListView.ItemsSource = DBEntities.GetContext().Service.ToList();
        }

        private void FilterRoleCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserAddBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (TypeOfServiceListView.SelectedIndex == 0)
                {
                    NavigationService.Navigate(new TypeOfWorkListID0());
                }
                if (TypeOfServiceListView.SelectedIndex == 1)
                {
                    NavigationService.Navigate(new TypeOfWorkListID1());
                }
                if (TypeOfServiceListView.SelectedIndex == 2)
                {
                    NavigationService.Navigate(new TypeOfWorkListID2());
                }
            }
        }
    }
}
