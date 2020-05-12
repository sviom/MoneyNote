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
        public async Task<bool> LeaveApp(User signinedUser)
        {
            if (signinedUser == null)
                return false;

            var result = await MoneyApi.DeleteUser.ApiLauncher<User, bool>(signinedUser, ControllerEnum.user);
            if (result.Result)
                return true;

            return false;
        }
    }
}
