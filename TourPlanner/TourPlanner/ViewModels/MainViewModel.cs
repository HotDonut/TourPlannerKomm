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
        public ICommand AddCommand => _addCommand ??= new RelayCommand(AddTour);
        private ICommand _removeCommand;
        public ICommand RemoveCommand => _removeCommand ??= new RelayCommand(RemoveTour);
        private ICommand _editCommand;
        public ICommand EditCommand => _editCommand ??= new RelayCommand(EditTour);
        private ICommand _copyCommand;
        public ICommand CopyCommand => _copyCommand ??= new RelayCommand(CopyTour);
        private ICommand _addLogCommand;
        public ICommand AddLogCommand => _addLogCommand ??= new RelayCommand(AddLog);


        public ObservableCollection<Tour> TourList { get; set; }
        public ObservableCollection<Log> LogList { get; set; }
        public string Name { get; set; }

        private Tour _currentTour;
        private Log _currentLog;


        public Tour CurrentTour
        {
            get => _currentTour;
            set
            {
                if (_currentTour != value)
                {
                    _currentTour = value;
                    RaisePropertyChangedEvent(nameof(CurrentTour));
                    LoadLogs(_currentTour);
                }
            }
        }

        public Log CurrentLog
        {
            get => _currentLog;
            set
            {
                if (_currentLog != value)
                {
                    _currentLog = value;
                    RaisePropertyChangedEvent(nameof(CurrentLog));
                }
            }
        }

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

        private void LoadLogs(Tour tour)
        {
            LogList.Clear();
            foreach (var log in this._tourPlannerFactory.GetTourLogs(tour))
            {
                LogList.Add(log);
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

        private void CopyTour(object commandParameter)
        {
            if (CurrentTour != null)
            {
                Tour tour = _tourPlannerFactory.CopyTour(CurrentTour);
                TourList.Add(tour);
                LoadTours();
            }
            else
            {
                MessageBox.Show("Please select the tour you want to copy!");
            }
        }

        private void AddLog(object commandParameter)
        {
            if (CurrentTour != null)
            {
                AddLogWindow addLogWindow = new AddLogWindow(this, CurrentTour);
                addLogWindow.Show();
            }
            else
            {
                MessageBox.Show("Please select the tour you want to copy!");
            }
        }
    }
}
