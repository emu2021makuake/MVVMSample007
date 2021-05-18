using MVVMSample007.ViewModels;
using System.Windows;

namespace MVVMSample007.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
