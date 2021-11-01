using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.ViewModels;

namespace TourPlanner.Windows
{
    /// <summary>
    /// Interaktionslogik für EditTourWindow.xaml
    /// </summary>
    public partial class EditTourWindow : Window
    {
        public EditTourWindow(MainViewModel mainView, Tour tour)
        {
            InitializeComponent();
            this.DataContext = new EditTourViewModel(this, tour, mainView);
        }

        private void NumbersOnly(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
