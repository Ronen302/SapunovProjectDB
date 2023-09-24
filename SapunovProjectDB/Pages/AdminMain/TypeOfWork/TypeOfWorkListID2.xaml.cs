using SapunovProjectDB.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SapunovProjectDB.Pages.AdminMain.TypeOfWork
{
    public partial class TypeOfWorkListID2 : Page
    {
        public TypeOfWorkListID2()
        {
            InitializeComponent();
            TypeOfWorkListView.ItemsSource = DBEntities.GetContext().TypeOfWork.
                Where(u => u.IdTypeOfService == 3).ToList();
        }

        private void UserAddBtn_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
