using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public sealed partial class MainSetting : UserControl
    {
        public MainSetting()
        {
            this.InitializeComponent();
        }
        
        private async void AllClearButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog();
            dialog.DefaultButton = ContentDialogButton.Close;
            dialog.Title = "전체 초기화를 진행하시겠습니까?";
            dialog.PrimaryButtonText = "예";
            dialog.CloseButtonText = "아니오";
            dialog.Content = "사용자의 금액 내역, 카테고리 등 모든 정보가 초기화됩니다.";
            await dialog.ShowAsync();
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

        private void Dialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
