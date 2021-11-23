using MoneyNoteLibrary5.Common;
using MoneyNoteLibrary5.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static MoneyNoteLibrary5.Enums.MoneyApiInfo;

namespace MoneyNoteLibrary5.ViewModels
{
    public class SettingViewModel : ViewModelBase
    {
        private bool _IsShowEndPage;
        public bool IsShowEndPage
        {
            get { return _IsShowEndPage; }
            set
            {
                if (_IsShowEndPage == value)
                    return;

                _IsShowEndPage = value;
                OnPropertyChanged();
            }
        }

        public async Task<bool> LeaveApp(User signinedUser)
        {
            if (signinedUser == null)
                return false;

            IsRunProgressRing = true;

            var result = await MoneyApi.DeleteUser.ApiLauncher<User, bool>(signinedUser, ControllerEnum.user);

            IsRunProgressRing = false;

            if (result.Result)
                return true;

            ErrorMessage = "탈퇴 과정에 오류가 발생했습니다.";
            return false;
        }

        public async Task<bool> ClearUserData(User user)
        {
            if (user == null)
                return false;

            IsRunProgressRing = true;

            var result = await MoneyApi.ClearUser.ApiLauncher<User, bool>(user, ControllerEnum.user);

            IsRunProgressRing = false;

            if (result.Result)
                return true;

            ErrorMessage = "초기화 과정에 오류가 발생했습니다.";
            return false;
        }

        public bool CheckEqualConfirmPassword(string newPassword, string newConfirmPassword)
        {
            if (newPassword.Equals(newConfirmPassword))
            {
                return true;
            }

            ErrorMessage = "입력한 비밀번호가 일치하지 않습니다.";
            return false;
        }

        public async Task<bool> ChangePassword(User user, string oldPassword, string newPassword, string newConfirmPassword)
        {
            if (user == null)
                return false;

            if (!CheckEqualConfirmPassword(newPassword, newConfirmPassword))
                return false;


            IsRunProgressRing = true;

            user.Password = UtilityLauncher.EncryptSHA256(newPassword);
            user.ConfirmPassword = UtilityLauncher.EncryptSHA256(newConfirmPassword);

            var result = await MoneyApi.ChangePassword.ApiLauncher<User, bool>(user, ControllerEnum.user);

            IsRunProgressRing = false;

            if (result.Result)
            {
                ErrorMessage = string.Empty;
                return true;
            }

            ErrorMessage = "초기화 과정에 오류가 발생했습니다.";
            return false;
        }
    }
}
