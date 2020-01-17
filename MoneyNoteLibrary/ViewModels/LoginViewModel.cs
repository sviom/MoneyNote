using MoneyNoteLibrary.Common;
using MoneyNoteLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static MoneyNoteLibrary.Enums.MoneyApiInfo;

namespace MoneyNoteLibrary.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ControllerEnum Controller => ControllerEnum.user;

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
            var encryptedPassword = UtilityLauncher.EncryptSHA256(Password);
            var tempUser = new User()
            {
                Email = Email,
                Password = encryptedPassword
            };

            var result = await MoneyApi.LogIn.ApiLauncher<User, User>(tempUser, Controller);
            return new Tuple<bool, User>(result.Result, result.Content);
        }
    }
}
