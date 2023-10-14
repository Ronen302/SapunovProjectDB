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
    /// <summary>
    /// Логика взаимодействия для TypeOfWorkList.xaml
    /// </summary>
    public partial class TypeOfWorkList : Page
    {
        public TypeOfWorkList()
        {
            InitializeComponent();
            TypeOfWorkListView.ItemsSource = DBEntities.GetContext().TypeOfWork.
                Where(u => u.IdService == Properties.Settings.Default.SelectedIdService).ToList();
        }

        private void FilterRoleCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UserAddBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
