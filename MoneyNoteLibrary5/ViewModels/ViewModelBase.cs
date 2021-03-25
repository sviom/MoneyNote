using MoneyNoteLibrary5.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace MoneyNoteLibrary5.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public User LoginedUser { get; set; }

        private string _ErrorMessage;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set
            {
                if (_ErrorMessage == value)
                    return;

                _ErrorMessage = value;
                OnPropertyChanged();
            }
        }

        private bool _IsRunProgressRing;
        public bool IsRunProgressRing
        {
            get { return _IsRunProgressRing; }
            set
            {
                if (_IsRunProgressRing == value)
                    return;

                _IsRunProgressRing = value;
                OnPropertyChanged();
            }
        }

    }
}
