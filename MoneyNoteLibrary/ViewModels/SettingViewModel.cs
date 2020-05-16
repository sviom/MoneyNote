﻿using MoneyNoteLibrary.Common;
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

            IsShowEndPage = true;

            var result = await MoneyApi.DeleteUser.ApiLauncher<User, bool>(signinedUser, ControllerEnum.user);

            IsShowEndPage = false;

            if (result.Result)
                return true;

            return false;
        }
    }
}
