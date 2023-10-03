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

namespace SapunovProjectDB.Pages.AdminMain.TypeOfWork
{
    public partial class TypeOfWorkListID0 : Page
    {
        public TypeOfWorkListID0()
        {
            InitializeComponent();
            TypeOfWorkListView.ItemsSource = DBEntities.GetContext().TypeOfWork.
                Where(u => u.IdTypeOfService == 1).ToList();
        }

        private void UserAddBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FilterRoleCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentIdUser = DBEntities.GetContext().User
                    .FirstOrDefault(u => u.LoginUser == Properties.Settings.Default.CurrentUser).IdUser;
                var currentIdClient = DBEntities.GetContext().Client.
                    FirstOrDefault(u => u.IdUser == currentIdUser).IdClient;
                var currentIdTypeOfWork = (int)(sender as Button).Tag;

                var newOrder = new Order()
                {
                    DateOfCreate = DateTime.Now,
                    IdClient = currentIdClient,
                    IdTypeOfWork = currentIdTypeOfWork,
                    IdStatusOrder = 1
                };
                DBEntities.GetContext().Order.Add(newOrder);
                DBEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                Error.ErrorMB(ex);
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
