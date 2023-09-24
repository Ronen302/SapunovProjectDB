using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
    /// Логика взаимодействия для UserAdd.xaml
    /// </summary>
    public partial class UserAdd : Page
    {
        public UserAdd()
        {
            InitializeComponent();
            UserRoleComboBox.ItemsSource = DBEntities.GetContext().Role.ToList();
        }

        private void userSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserLoginTextBox.Text) &&
                string.IsNullOrWhiteSpace(UserPasswordTextBox.Text) &&
                UserRoleComboBox.SelectedItem == null)
            {
                EmptyLoginErrorAddUser.Visibility = Visibility.Visible;
                EmptyPasswordErrorAddUser.Visibility = Visibility.Visible;
                EmptyRoleErrorAddUser.Visibility = Visibility.Visible;
            }
            else
            {
                try
                {
                    var addNewUser = new User()
                    {
                        LoginUser = UserLoginTextBox.Text,
                        PasswordUser = UserPasswordTextBox.Text,
                        IdRole = Int32.Parse(UserRoleComboBox.SelectedValue.ToString())
                    };
                    DBEntities.GetContext().User.Add(addNewUser);
                    DBEntities.GetContext().SaveChanges();
                    NavigationService.Navigate(new UserList());
                    NavigationService.Navigate(new UserAdd());
                }
                catch (Exception ex)
                {
                    Error.ErrorMB(ex);
                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new UserList());
            Content = null;
        }

        private void UserLoginTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EmptyLoginErrorAddUser.Visibility = Visibility.Collapsed;
        }

        private void UserPasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EmptyPasswordErrorAddUser.Visibility = Visibility.Collapsed;
            UserPasswordTextBox.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#f2f3f5");
            UserPasswordTextBox.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#d5d5d7");
        }

        private void UserRoleComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            EmptyRoleErrorAddUser.Visibility = Visibility.Collapsed;
        }
    }
}
