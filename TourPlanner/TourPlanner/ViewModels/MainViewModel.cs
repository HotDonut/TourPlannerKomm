using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
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
        private ICommand _removeCommand;
        private ICommand _editCommand;

        public ICommand AddCommand => _addCommand ??= new RelayCommand(AddTour);
        public ICommand RemoveCommand => _removeCommand ??= new RelayCommand(RemoveTour);
        public ICommand EditCommand => _editCommand ??= new RelayCommand(EditTour);

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

        private void AddTour(object commandParameter)
        {
            AddTourWindow addTourWindow = new AddTourWindow(this);
            addTourWindow.Show();
        }

        private void RemoveTour(object commandParameter)
        {
            string imagePath = CurrentTour.ImagePath;
            CurrentTour.ImagePath = null;
            RaisePropertyChangedEvent(nameof(CurrentTour));
            _tourPlannerFactory.DeleteTour(CurrentTour, imagePath);
            CurrentTour = null;

            LoadTours();
        }

        private void EditTour(object commandParameter)
        {
            if (CurrentTour != null)
            {
                EditTourWindow editTourWindow = new EditTourWindow(this, CurrentTour);
                editTourWindow.Show();
            } else  {
                MessageBox.Show("Please select the tour you want to edit!");
            }           
        }
    }
}
