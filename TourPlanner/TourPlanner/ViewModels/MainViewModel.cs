using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections;
using System.Collections.ObjectModel;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        public ObservableCollection<Tour> Tours { get; set; }
        public string Name { get; set; }
        private string higherOrLower;

        public string HigherOrLower
        {
            get { return higherOrLower; }
            set
            {
                if (higherOrLower != value)
                {
                    higherOrLower = value;
                    RaisePropertyChangedEvent(nameof(HigherOrLower));
                }
            }
        }

        public MainViewModel()
        {
            Tours = new ObservableCollection<Tour>();
            Tour a = new Tour(0, "Test", "TestTour", "Vienna", "Graz", 200, "");
            Tour b = new Tour(0, "Test", "TestTour", "Vienna", "Graz", 200, "");
            Tours.Add(a);
            Tours.Add(b);
        }



    }

}
