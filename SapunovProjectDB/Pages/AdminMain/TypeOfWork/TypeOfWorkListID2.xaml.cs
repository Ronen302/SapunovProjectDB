using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
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
                Where(u => u.IdService == 3).ToList();
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
