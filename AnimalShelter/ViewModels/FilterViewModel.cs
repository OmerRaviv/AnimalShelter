﻿using System;
using System.Collections.Generic;
using AnimalShelter.Model;
using SuiteValue.UI.MVVM;

namespace AnimalShelter.ViewModels
{
    public class FilterViewModel : ViewModelBase
    {
        private bool _fromAgeIsChecked;

        public bool FromAgeIsChecked
        {
            get { return _fromAgeIsChecked; }
            set
            {
                if (value != _fromAgeIsChecked)
                {
                    _fromAgeIsChecked = value;
                    OnPropertyChanged(() => FromAgeIsChecked);
                    FilterCommand.Refresh();
                }
            }
        }

        private double _fromAge;

        public double FromAge
        {
            get { return _fromAge; }
            set
            {
                if (value != _fromAge)
                {
                    _fromAge = value;
                    OnPropertyChanged(() => FromAge);
                }
            }
        }

        private bool _toAgeIsChecked;

        public bool ToAgeIsChecked
        {
            get { return _toAgeIsChecked; }
            set
            {
                if (value != _toAgeIsChecked)
                {
                    _toAgeIsChecked = value;
                    OnPropertyChanged(() => ToAgeIsChecked);
                    FilterCommand.Refresh();
                }
            }
        }

        private double _toAge;

        public double ToAge
        {
            get { return _toAge; }
            set
            {
                if (value != _toAge)
                {
                    _toAge = value;
                    OnPropertyChanged(() => ToAge);
                }
            }
        }


        private DelegateCommand _filterCommand;

        public DelegateCommand FilterCommand
        {
            get
            {
                return _filterCommand ?? (_filterCommand = new DelegateCommand(
                    () =>
                    {
                        var list = new List<Func<Dog,bool>>();
                        if (FromAgeIsChecked)
                        {
                            list.Add(d => d.Age > FromAge);
                        }
                        if (ToAgeIsChecked)
                        {
                            list.Add(d => d.Age < ToAge);
                        }
                        Filter = list.ToArray();
                        OnFilter(false);
                    },
                    () =>
                    {
                        return FromAgeIsChecked || ToAgeIsChecked;
                    }));
            }
        }

        private DelegateCommand _clearCommand;

        public DelegateCommand ClearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new DelegateCommand(
                    () =>
                    {
                        FromAge = 0;
                        FromAgeIsChecked = false;
                        ToAge = 0;
                        ToAgeIsChecked = false;
                        Filter = null;
                        OnFilter(true);
                    }));
            }
        }




        public Func<Dog,bool>[] Filter { get; set; }

        public event Action<bool> OnFilter = delegate {  };




    }
}