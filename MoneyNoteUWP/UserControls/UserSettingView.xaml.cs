﻿using MoneyNote.Pages;
using MoneyNoteLibrary.ViewModels;
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

// 사용자 정의 컨트롤 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=234236에 나와 있습니다.

namespace MoneyNote.UserControls
{
    public sealed partial class UserSettingView : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private SettingViewModel _ViewModel;
        public SettingViewModel ViewModel
        {
            get { return _ViewModel; }
            set
            {
                if (_ViewModel == value)
                    return;

                _ViewModel = value;
                OnPropertyChanged();
            }
        }

        public UserSettingView()
        {
            this.InitializeComponent();
            this.Loaded += MainSetting_Loaded;
            this.Unloaded += MainSetting_Unloaded;
        }

        private void MainSetting_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new SettingViewModel();
        }

        private void MainSetting_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private async void AllClearButton_Click(object sender, RoutedEventArgs e)
        {
            var clearDialog = new ContentDialog();
            clearDialog.DefaultButton = ContentDialogButton.Close;
            clearDialog.Title = "전체 초기화를 진행하시겠습니까?";
            clearDialog.PrimaryButtonText = "예";
            clearDialog.CloseButtonText = "아니오";
            clearDialog.Content = "사용자의 금액 내역, 카테고리 등 모든 정보가 초기화됩니다.";

            clearDialog.PrimaryButtonClick += ClearDialog_PrimaryButtonClick;

            await clearDialog.ShowAsync();
        }

        private async void ClearDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var result = await ViewModel.ClearUserData(App.LogInedUser);
            if (result)
            {
                var resultDialog = new ContentDialog
                {
                    DefaultButton = ContentDialogButton.Close,
                    Title = "초기화 완료됨",
                    PrimaryButtonText = "확인",
                    Content = "초기화가 완료되었습니다."
                };
                resultDialog.PrimaryButtonClick += ResultDialog_PrimaryButtonClick;
                await resultDialog.ShowAsync();
            }
        }

        private void ResultDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            HomePage.CurrentHomePage.MenuContent.Navigate(typeof(MoneyBasicListPage));
        }

        private async void LeaveApp_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog();
            dialog.DefaultButton = ContentDialogButton.Close;

            dialog.Title = "정말 탈퇴하시겠습니까? ";
            dialog.PrimaryButtonText = "예";
            dialog.CloseButtonText = "아니오";
            dialog.Content = "사용자의 기록 정보 등 모든 정보가 삭제되며, 복구할 수 없습니다. ";
            dialog.PrimaryButtonClick += Dialog_PrimaryButtonClick;
            await dialog.ShowAsync();
        }

        private async void Dialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var result = await ViewModel.LeaveApp(App.LogInedUser);
            if (result)
                ViewModel.IsShowEndPage = true;
        }

        private void ExitAppButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
