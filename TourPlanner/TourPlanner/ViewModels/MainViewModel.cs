using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections;
using System.Collections.ObjectModel;
using TourPlanner.Models;
using TourPlanner.Windows;

namespace TourPlanner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ICommand _addCommand;

        public ICommand AddCommand => _addCommand ??= new RelayCommand(Add);

        public ObservableCollection<Tour> Tours { get; set; }
        public string Name { get; set; }

        public MainViewModel()
        {
            Tours = new ObservableCollection<Tour>();
            Tour a = new Tour(0, "Test", "TestTour", "Vienna", "Graz", 200, "");
            Tour b = new Tour(0, "Test", "TestTour", "Vienna", "Graz", 200, "");
            Tours.Add(a);
            Tours.Add(b);
        }

        private void Add(object commandParameter)
        {
            AddTourWindow addTourWindow = new AddTourWindow(this);
            addTourWindow.Show();
        }

    }

}
