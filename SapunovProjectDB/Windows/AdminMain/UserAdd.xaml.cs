using SapunovProjectDB.Classes;
using SapunovProjectDB.Data;
using SapunovProjectDB.Pages;
using SapunovProjectDB.Pages.AdminMain;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace SapunovProjectDB.Windows.AdminMain
{
    /// <summary>
    /// Логика взаимодействия для UserAdd.xaml
    /// </summary>
    public partial class UserAdd : Window
    {
        User newUser = new User();
        public UserAdd(User selectedUser)
        {
            InitializeComponent();
            DataContext = newUser;
            if (selectedUser != null)
                newUser = selectedUser;
            UserRoleComboBox.ItemsSource = DBEntities.GetContext().Role.ToList();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void userSaveButton_Click(object sender, RoutedEventArgs e)
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
                Close();
            }
            catch (Exception ex)
            {
                Error.ErrorMB(ex);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
