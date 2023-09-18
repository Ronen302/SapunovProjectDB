using System;
using System.Windows;

namespace SapunovProjectDB.Classes
{
    public class Error
    {
        public static void ErrorMB(Exception exception)
        {
            MessageBox.Show(exception.Message,
                "Ошибка", MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
