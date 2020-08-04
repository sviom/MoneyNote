using MoneyNoteLibrary.Common;
using MoneyNoteLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static MoneyNoteLibrary.Enums.MoneyApiInfo;

namespace MoneyNoteLibrary.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private string _NickName;
        public string NickName
        {
            get { return _NickName; }
            set
            {
                if (_NickName == value)
                    return;

                _NickName = value;
                OnPropertyChanged();
                ValidCheck();
            }
        }

        private string _EmailAddress;
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set
            {
                if (_EmailAddress == value)
                    return;

                _EmailAddress = value;
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

        private string _ConfirmPassword;
        public string ConfirmPassword
        {
            get { return _ConfirmPassword; }
            set
            {
                if (_ConfirmPassword == value)
                    return;

                _ConfirmPassword = value;
                OnPropertyChanged();
                ValidCheck();
                OnPropertyChanged(nameof(IsNotEqualPassword));
            }
        }

        public bool IsValidPassword => Password != null && (Password.Length > 8 && Password.Equals(ConfirmPassword));

        public string IsNotEqualPassword => Password != null && Password.Equals(ConfirmPassword) ? string.Empty : "비밀번호가 일치하지 않습니다.";

        public bool IsSignUpEnable => Common.ValidCheck.IsValidEmail(EmailAddress) && IsValidPassword && IsValidNickName;

        public bool IsValidNickName => !string.IsNullOrEmpty(NickName);

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

        public SignUpViewModel() { }

        public void ValidCheck()
        {
            OnPropertyChanged(nameof(IsValidPassword));
            OnPropertyChanged(nameof(IsValidNickName));
            OnPropertyChanged(nameof(IsSignUpEnable));
        }

        public async Task<(bool, User)> SignUp()
        {
            var result = false;
            var encryptedPassword = UtilityLauncher.EncryptSHA256(Password);

            var signUpUser = new User()
            {
                Email = EmailAddress,
                Name = NickName,
                Password = encryptedPassword
            };

            IsRunProgressRing = true;

            var signUpResult = await MoneyApi.SignUp.ApiLauncher<User, User>(signUpUser, ControllerEnum.user);
            if (signUpResult.Result)
            {
                result = true;
            }
            else
            {
                ErrorMessage = !string.IsNullOrEmpty(signUpResult.ResultMessage) ? signUpResult.ResultMessage : "에러!!";
            }

            IsRunProgressRing = false;

            return (result, signUpResult.Content);
        }
    }
}
