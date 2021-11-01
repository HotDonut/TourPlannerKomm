﻿using System;
using System.Windows;
using System.Windows.Input;
using TourPlanner.BusinessLayer;
using TourPlanner.Models;

namespace TourPlanner.ViewModels
{
    class EditTourViewModel : ViewModelBase
    {
        private Window _window;
        private MainViewModel _mainView;
        private ITourPlannerFactory _tourPlannerFactory;

        private Tour _tour;

        private string _tourName;
        private string _tourDescription;
        private string _tourFromLocation;
        private string _tourToLocation;
        private int _tourDistance;

        private ICommand _editTourCommand;
        private ICommand _cancelTourCommand;


        public ICommand EditTourCommand => _editTourCommand ??= new RelayCommand(EditTour);
        public ICommand CancelTourCommand => _cancelTourCommand ??= new RelayCommand(CancelTour);

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

        public String TourDescription
        {
            get => _tourDescription;
            set
            {
                if (_tourDescription != value)
                {
                    _tourDescription = value;
                    RaisePropertyChangedEvent(nameof(TourDescription));
                }
            }
        }

        public String TourFromLocation
        {
            get => _tourFromLocation;
            set
            {
                if (_tourFromLocation != value)
                {
                    _tourFromLocation = value;
                    RaisePropertyChangedEvent(nameof(TourFromLocation));
                }
            }
        }

        public String TourToLocation
        {
            get => _tourToLocation;
            set
            {
                if (_tourToLocation != value)
                {
                    _tourToLocation = value;
                    RaisePropertyChangedEvent(nameof(TourToLocation));
                }
            }
        }

        public EditTourViewModel(Window window, Tour tour, MainViewModel mainView)
        {
            _window = window;
            _mainView = mainView;
            _tour = tour;
            _tourName = tour.Name;
            _tourDescription = tour.Description;
            _tourFromLocation = tour.FromLocation;
            _tourToLocation = tour.ToLocation;

            this._tourPlannerFactory = TourPlannerFactory.GetInstance();
        }

        private void EditTour(object commandParameter)
        {
            Tour tour = _tourPlannerFactory.EditTour(_tour, TourName, TourDescription, TourFromLocation, TourToLocation);
            if (tour != null)
            {
                _mainView.TourList.Remove(_tour);
                _mainView.TourList.Add(tour);
            }
            _window.Close();
        }

        private void CancelTour(object commandParameter)
        {
            _window.Close();
        }
    }
}