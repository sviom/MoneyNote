﻿using MoneyNoteLibrary5.Common;
using MoneyNoteLibrary5.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MoneyNoteLibrary5.Enums.MoneyApiInfo;

namespace MoneyNoteLibrary5.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public const string SavedIdTextFile = "MoneyNote_id_save.txt";

        public ControllerEnum Controller => ControllerEnum.user;

        private bool _IsIdSaveChecked;
        public bool IsIdSaveChecked
        {
            get { return _IsIdSaveChecked; }
            set
            {
                if (_IsIdSaveChecked == value)
                    return;

                _IsIdSaveChecked = value;
                OnPropertyChanged();
            }
        }

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

        public async Task<Tuple<bool, User>> LogIn()
        {
            IsRunProgressRing = true;

            var encryptedPassword = UtilityLauncher.EncryptSHA256(Password);
            var tempUser = new User()
            {
                Email = Email,
                Password = encryptedPassword
            };

            var result = await MoneyApi.LogIn.ApiLauncher<User, User>(tempUser, Controller);

            if (!result.Result)
                ErrorMessage = !string.IsNullOrEmpty(result.ResultMessage) ? result.ResultMessage : "에러가 발생했습니다.";

            IsRunProgressRing = false;
            return new Tuple<bool, User>(result.Result, result.Content);
        }
    }


    public class SaveIdForm
    {
        public string Id { get; set; }

        public bool IsSaveChecked { get; set; }
    }
}
