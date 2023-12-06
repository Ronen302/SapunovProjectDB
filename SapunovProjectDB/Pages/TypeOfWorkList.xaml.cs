using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using SapunovProjectDB.Windows;
using SapunovProjectDB.Windows.AddEditWindows;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SapunovProjectDB.Pages
{
    public partial class TypeOfWorkList : Page
    {
        public TypeOfWorkList()
        {
            InitializeComponent();
            UpdateFilter();
        }
        private void UpdateFilter()
        {
            try
            {
                var currentTypeOfWork = DBEntities.GetContext().TypeOfWork.
                Where(u => u.IdService == Properties.Settings.Default.SelectedIdService).ToList();
                currentTypeOfWork = currentTypeOfWork.Where(u => u.NameTypeOfWork
                .StartsWith(textFilter.Text)).ToList();
                TypeOfWorkListView.ItemsSource = currentTypeOfWork.OrderBy(u => u.IdTypeOfWork);
            }
            catch (Exception ex)
            {
                Error.ErrorMB(ex);
            }
        }

        private void UserAddBtn_Click(object sender, RoutedEventArgs e)
        {
            TypeOfWorkAddEdit typeOfWorkEdit = new TypeOfWorkAddEdit(null);
            if (typeOfWorkEdit.ShowDialog() == true)
            {
                UpdateFilter();
                DataIsSaved();
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            TypeOfWorkAddEdit typeOfWorkEdit = new TypeOfWorkAddEdit((sender as Button).DataContext as TypeOfWork);
            if (typeOfWorkEdit.ShowDialog() == true)
            {
                UpdateFilter();
                DataIsSaved();
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            TypeOfWork typeOfWork = TypeOfWorkListView.SelectedItem as TypeOfWork;
            RemoveDialogWindow removeDialog = new RemoveDialogWindow();
            removeDialog.removeMessage.Text = $"Услуга будет удалена без возможности восстановления." +
                        $" Вы действительно желаете это сделать?";
            if (removeDialog.ShowDialog() == true)
            {
                try
                {
                    DBEntities.GetContext().TypeOfWork.Remove(typeOfWork);
                    DBEntities.GetContext().SaveChanges();
                    UpdateFilter();
                }
                catch (Exception ex)
                {
                    Error.ErrorMB(ex);
                }
            }
        }

        private void addOrderButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int _currentIdUser = DBEntities.GetContext().User
                    .FirstOrDefault(u => u.LoginUser == Properties.Settings.Default.CurrentUser).IdUser;
                int _currentIdClient = DBEntities.GetContext().Client
                    .FirstOrDefault(u => u.IdUser == _currentIdUser).IdClient;
                int _currentIdTypeOfWork = (int)(sender as Button).Tag;
                var newOrder = new Order()
                {
                    IdClient = _currentIdClient,
                    IdTypeOfWork = _currentIdTypeOfWork,
                    IdStatusOrder = 1,
                    DateOfCreate = DateTime.Now,
                };
                DBEntities.GetContext().Order.Add(newOrder);
                DBEntities.GetContext().SaveChanges();
                DataIsSaved();
            }
            catch (Exception ex)
            {
                Error.ErrorMB(ex);
            }
        }
        private async void DataIsSaved()
        {
            dataIsSavedMessage.Text = "Заказ успешно оформлен";
            dataIsSavedMessage.Visibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(2.6));
            dataIsSavedMessage.Visibility = Visibility.Collapsed;
            dataIsSavedMessage.Text = "Данные сохранены";
        }

        private void textFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateFilter();
        }

        private void backToServiceButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
