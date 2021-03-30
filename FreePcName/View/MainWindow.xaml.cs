using System.Windows;
using System.Windows.Controls;

namespace FreePcName
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is ListBox lb)
            {
                Clipboard.SetData(DataFormats.Text, lb.SelectedItem);
            }
        }
    }
}
