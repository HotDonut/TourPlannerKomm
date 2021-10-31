using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TourPlanner.ViewModels;

namespace TourPlanner.Windows {
    /// <summary>
    /// Interaktionslogik für AddTourWindow.xaml
    /// </summary>
    public partial class AddTourWindow : Window {
        public AddTourWindow(MainViewModel mainView)
        {
            InitializeComponent();
            this.DataContext = new AddTourViewModel(this, mainView);
        }

        private void NumbersOnly(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}