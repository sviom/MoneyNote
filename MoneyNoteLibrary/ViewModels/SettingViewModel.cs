using MoneyNoteLibrary.Common;
using MoneyNoteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static MoneyNoteLibrary.Enums.MoneyApiInfo;

namespace MoneyNoteLibrary.ViewModels
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
    }
}
