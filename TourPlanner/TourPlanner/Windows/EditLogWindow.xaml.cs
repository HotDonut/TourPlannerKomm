using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.ViewModels;

namespace TourPlanner.Windows {
    /// <summary>
    /// Interaktionslogik für EditLogWindow.xaml
    /// </summary>
    public partial class EditLogWindow : Window {
        public EditLogWindow(MainViewModel mainView, Tour tour, Log log)
        {
            InitializeComponent();
            this.DataContext = new EditLogViewModel(this, tour, log, mainView);
        }

        private void NumbersOnly(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
