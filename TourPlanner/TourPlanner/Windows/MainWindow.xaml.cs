using System.Windows;
using TourPlanner.ViewModels;

namespace TourPlanner.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
            this.DataContext = new MainViewModel();
        }
    }
}
