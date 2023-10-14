using System.Windows;

namespace SapunovProjectDB.Windows
{
    public partial class RemoveDialogWindow : Window
    {
        public RemoveDialogWindow()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void MainBorder_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
