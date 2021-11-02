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
using TourPlanner.Logger;

namespace TourPlanner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ITourPlannerFactory _tourPlannerFactory;

        private static readonly log4net.ILog _log = LogHelper.GetLogger();

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
        private ICommand _editLogCommand;
        public ICommand EditLogCommand => _editLogCommand ??= new RelayCommand(EditLog);
        private ICommand _removeLogCommand;
        public ICommand RemoveLogCommand => _removeLogCommand ??= new RelayCommand(RemoveLog);
        private ICommand _copyLogCommand;
        public ICommand CopyLogCommand => _copyLogCommand ??= new RelayCommand(CopyLog);
        private ICommand _printTourCommand;
        public ICommand PrintTourCommand => _printTourCommand ??= new RelayCommand(PrintTour);
        private ICommand _printAllCommand;
        public ICommand PrintAllCommand => _printAllCommand ??= new RelayCommand(PrintAll);


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
            _log.Info("All tours loaded.");
        }

        private void LoadLogs(Tour tour)
        {
            LogList.Clear();
            foreach (var log in this._tourPlannerFactory.GetTourLogs(tour))
            {
                LogList.Add(log);
            }
            _log.Info("All logs loaded.");
        }

        private void AddTour(object commandParameter)
        {
            AddTourWindow addTourWindow = new AddTourWindow(this);
            addTourWindow.Show();
            _log.Info("Tour adding process started.");
        }

        private void RemoveTour(object commandParameter)
        {
            string imagePath = CurrentTour.ImagePath;
            CurrentTour.ImagePath = null;
            RaisePropertyChangedEvent(nameof(CurrentTour));
            _tourPlannerFactory.DeleteTour(CurrentTour, imagePath);
            CurrentTour = null;

            _log.Info("Tour removed.");

            LoadTours();
        }

        private void EditTour(object commandParameter)
        {
            if (CurrentTour != null)
            {
                EditTourWindow editTourWindow = new EditTourWindow(this, CurrentTour);
                editTourWindow.Show();
                _log.Info("Tour editing process started.");
            }
            else
            {
                MessageBox.Show("Please select the tour you want to edit!");
                _log.Warn("Tour editing process could not be started.");
            }
        }

        private void CopyTour(object commandParameter)
        {
            if (CurrentTour != null)
            {
                Tour tour = _tourPlannerFactory.CopyTour(CurrentTour);
                TourList.Add(tour);
                _log.Info("Tour copying process started.");
                LoadTours();
            }
            else
            {
                MessageBox.Show("Please select the tour you want to copy!");
                _log.Warn("Tour copying process could not be started.");
            }
        }

        private void AddLog(object commandParameter)
        {
            if (CurrentTour != null)
            {
                AddLogWindow addLogWindow = new AddLogWindow(this, CurrentTour);
                addLogWindow.Show();
                _log.Info("Log adding process started.");
            }
            else
            {
                MessageBox.Show("Please select the tour you want to copy!");
                _log.Warn("Log adding process could not be started.");
            }
        }

        private void EditLog(object commandParameter)
        {
            if (CurrentLog != null)
            {
                EditLogWindow editLogWindow = new EditLogWindow(this, CurrentTour, CurrentLog);
                editLogWindow.Show();
                _log.Info("Log editing process started.");
            }
            else
            {
                MessageBox.Show("Please select the log you want to edit!");
                _log.Warn("Log editing process could not be started.");
            }
        }

        private void RemoveLog(object commandParameter)
        {
            if (CurrentLog != null)
            {
                _tourPlannerFactory.DeleteTourLog(CurrentLog);
                CurrentLog = null;
                LoadLogs(CurrentTour);
                _log.Info("Log removed.");
            }
            else
            {
                MessageBox.Show("Please select the log you want to delete!");
                _log.Warn("Log could not be removed.");
            }
        }

        private void CopyLog(object commandParameter)
        {
            if (CurrentLog != null)
            {
                Log log = _tourPlannerFactory.CopyTourLog(CurrentTour, CurrentLog);
                LogList.Add(log);
                LoadLogs(CurrentTour);
                _log.Info("Log copied.");
            }
            else
            {
                MessageBox.Show("Please select the log you want to copy!");
                _log.Warn("Log could not be copied.");
            }
        }

        private void PrintTour(object commandParameter)
        {
            if (CurrentTour != null)
            {
                if (_tourPlannerFactory.PrintData(CurrentTour))
                {
                    _log.Info("PDF successfully created for one tour.");
                    MessageBox.Show("Successfully created PDF at location specified in the config file");
                }
                else
                {
                    _log.Warn("PDF creation failed.");
                    MessageBox.Show("Could not create PDF for one tour.");
                }
            }
            else
            {
                _log.Warn("PDF creation failed (No Tour selected).");
                MessageBox.Show("Please select the tour you want to print!");
            }
        }

        private void PrintAll(object commandParameter)
        {
            if (_tourPlannerFactory.PrintAllData())
            {
                _log.Info("PDF successfully created for all tours.");
                MessageBox.Show("Successfully created PDF at location specified in the config file");
            } 
            else
            {
                MessageBox.Show("Could not create PDF for all tours.");
            }
        }
    }
}
