using MoneyNoteLibrary.Common;
using MoneyNoteLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static MoneyNoteLibrary.Enums.MoneyApiInfo;

// 빈 페이지 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=234238에 나와 있습니다.

namespace MoneyNoteAdmin.Pages
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    public sealed partial class UserManagePage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<User> _AllUserList;
        public List<User> AllUserList
        {
            get { return _AllUserList; }
            set
            {
                if (_AllUserList == value)
                    return;

                _AllUserList = value;
                OnPropertyChanged();
            }
        }

        public UserManagePage()
        {
            this.InitializeComponent();
            this.Loaded += UserManagePage_Loaded;
            this.Unloaded += UserManagePage_Unloaded;
        }

        private void UserManagePage_Loaded(object sender, RoutedEventArgs e)
        {
            AllUserList = new List<User>();
            GetUsers();
        }

        private void UserManagePage_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        public async void GetUsers()
        {
            var result = await MoneyApi.GetUsers.ApiLauncher<bool, List<User>>(true, ControllerEnum.user);
            if (result.Result)
                AllUserList = result.Content;
        }

        private void UserApproveButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}