using System;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    class EditLogViewModel : ViewModelBase
    {

        private Window _window;
        private MainViewModel _mainView;
        private ITourPlannerFactory _tourPlannerFactory;

        private Log _tourLog;

        private string _tourName;

        private string _dateTime;
        private string _report;
        private int _distance;
        private string _totalTime;
        private int _rating;
        private int _breaks;
        private string _weather;
        private int _fuelConsumption;
        private string _passenger;
        private int _elevation;

        private ICommand _editLogCommand;
        private ICommand _cancelLogCommand;


        public ICommand EditLogCommand => _editLogCommand ??= new RelayCommand(EditLog);
        public ICommand CancelLogCommand => _cancelLogCommand ??= new RelayCommand(CancelLog);

        public String TourName
        {
            get => _tourName;
            set
            {
                if (_tourName != value)
                {
                    _tourName = value;
                    RaisePropertyChangedEvent(nameof(TourName));
                }
            }
        }

        public String DateTime
        {
            get => _dateTime;
            set
            {
                if (_dateTime != value)
                {
                    _dateTime = value;
                    RaisePropertyChangedEvent(nameof(DateTime));
                }
            }
        }

        public String Report
        {
            get => _report;
            set
            {
                if (_report != value)
                {
                    _report = value;
                    RaisePropertyChangedEvent(nameof(Report));
                }
            }
        }

        public int Distance
        {
            get => _distance;
            set
            {
                if (_distance != value)
                {
                    _distance = value;
                    RaisePropertyChangedEvent(nameof(Distance));
                }
            }
        }

        public String TotalTime
        {
            get => _totalTime;
            set
            {
                if (_totalTime != value)
                {
                    _totalTime = value;
                    RaisePropertyChangedEvent(nameof(TotalTime));
                }
            }
        }
        public int Rating
        {
            get => _rating;
            set
            {
                if (_rating != value)
                {
                    _rating = value;
                    RaisePropertyChangedEvent(nameof(Rating));
                }
            }
        }
        public int Breaks
        {
            get => _breaks;
            set
            {
                if (_breaks != value)
                {
                    _breaks = value;
                    RaisePropertyChangedEvent(nameof(Breaks));
                }
            }
        }
        public string Weather
        {
            get => _weather;
            set
            {
                if (_weather != value)
                {
                    _weather = value;
                    RaisePropertyChangedEvent(nameof(Weather));
                }
            }
        }
        public int FuelConsumption
        {
            get => _fuelConsumption;
            set
            {
                if (_fuelConsumption != value)
                {
                    _fuelConsumption = value;
                    RaisePropertyChangedEvent(nameof(FuelConsumption));
                }
            }
        }
        public string Passenger
        {
            get => _passenger;
            set
            {
                if (_passenger != value)
                {
                    _passenger = value;
                    RaisePropertyChangedEvent(nameof(Passenger));
                }
            }
        }
        public int Elevation
        {
            get => _elevation;
            set
            {
                if (_elevation != value)
                {
                    _elevation = value;
                    RaisePropertyChangedEvent(nameof(Elevation));
                }
            }
        }

        public EditLogViewModel(Window window, Tour tour, Log log, MainViewModel mainView)
        {
            _window = window;
            _mainView = mainView;
            _tourLog = log;
            _tourName = tour.Name;
            _dateTime = log.DateTime;
            _report = log.Report;
            _distance = log.Distance;
            _totalTime = log.TotalTime;
            _rating = log.Rating;
            _breaks = log.Breaks;
            _weather = log.Weather;
            _fuelConsumption = log.FuelConsumption;
            _passenger = log.Passenger;
            _elevation = log.Elevation;

            this._tourPlannerFactory = TourPlannerFactory.GetInstance();
        }

        private void EditLog(object commandParameter)
        {
            Log tourLog = _tourPlannerFactory.EditTourLog(_tourLog, DateTime, Report, Distance, TotalTime, Rating, Breaks, Weather, FuelConsumption, Passenger, Elevation);
            if (tourLog != null)
            {
                _mainView.LogList.Remove(_tourLog);
                _mainView.LogList.Add(tourLog);
            }

            _window.Close();
        }

        private void CancelLog(object commandParameter)
        {
            _window.Close();
        }
    }
}