using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace MoneyNoteLibrary.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _Email;
        public string Email
        {
            get { return _Email; }
            set
            {
                if (_Email == value)
                    return;

                _Email = value;
                OnPropertyChanged();
                ValidCheck();
            }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                if (_Password == value)
                    return;

                _Password = value;
                OnPropertyChanged();
                ValidCheck();
            }
        }

        public bool IsValidEmail => Common.ValidCheck.IsValidEmail(Email);

        public bool IsValidPassword => Password?.Length > 8;

        public bool IsEnableLogin => IsValidEmail && IsValidPassword;

        public void ValidCheck()
        {
            OnPropertyChanged(nameof(IsValidEmail));
            OnPropertyChanged(nameof(IsValidPassword));
            OnPropertyChanged(nameof(IsEnableLogin));
        }

        public bool LogIn()
        {
            return true;
        }
    }
}
