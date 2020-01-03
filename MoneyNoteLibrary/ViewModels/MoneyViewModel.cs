using MoneyNoteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MoneyNoteLibrary.ViewModels
{
    public class MoneyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<MoneyItem> _MoneyList;
        public ObservableCollection<MoneyItem> MoneyList
        {
            get { return _MoneyList; }
            set
            {
                if (_MoneyList == value)
                    return;

                _MoneyList = value;
                OnPropertyChanged();
            }
        }

        private double _Money;
        public double Money
        {
            get { return _Money; }
            set
            {
                if (_Money == value)
                    return;

                _Money = value;
                OnPropertyChanged();
            }
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                if (_Title == value)
                    return;

                _Title = value;
                OnPropertyChanged();
            }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description == value)
                    return;

                _Description = value;
                OnPropertyChanged();
            }
        }

        public MoneyViewModel()
        {
            Initialize();
        }

        public MoneyViewModel(MoneyItem item)
        {
            Initialize();
            SetViewModel(item);
        }

        public void Initialize()
        {
            MoneyList = new ObservableCollection<MoneyItem>();
        }

        public void SetViewModel(MoneyItem item)
        {
            Title = item.Title;
            Description = item.Description;
            Money = item.Money;
        }

        public void SaveMoney()
        {

        }

        public void ModifyMoney()
        {

        }
    }
}
