using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections;
using System.Collections.ObjectModel;
using TourPlanner.Models;
using TourPlanner.Windows;
using TourPlanner.BusinessLayer;

namespace TourPlanner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ITourPlannerFactory _tourPlannerFactory;

        private ICommand _addCommand;

        public ICommand AddCommand => _addCommand ??= new RelayCommand(Add);

        public ObservableCollection<Tour> TourList { get; set; }
        public ObservableCollection<Log> LogList { get; set; }
        public string Name { get; set; }

        private Tour _currentTour;



        public MainViewModel()
        {
            this._tourPlannerFactory = TourPlannerFactory.GetInstance();
            TourList = new ObservableCollection<Tour>();
            LogList = new ObservableCollection<Log>();
            LoadTours();
        }

        private void LoadTours()
        {
            TourList.Clear();
            foreach (var tour in this._tourPlannerFactory.GetTours())
            {
                TourList.Add(tour);
            }
        }

        public Tour CurrentTour
        {
            get => _currentTour;
            set
            {
                if (_currentTour != value)
                {
                    _currentTour = value;
                    RaisePropertyChangedEvent(nameof(CurrentTour));
                }
            }
        }

        private void Add(object commandParameter)
        {
            AddTourWindow addTourWindow = new AddTourWindow(this);
            addTourWindow.Show();
        }

    }

}
