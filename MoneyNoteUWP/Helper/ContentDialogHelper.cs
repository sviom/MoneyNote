using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace MoneyNote.Helper
{
    public class ContentDialogHelper
    {


        public async void ShowContentDialog()
        {
            var dialog = new ContentDialog();
            dialog.Title = "전체 초기화를 진행하시겠습니까?";
            dialog.PrimaryButtonText = "예";
            dialog.SecondaryButtonText = "아니오";
            dialog.Content = "사용자의 금액 내역, 카테고리 등 모든 정보가 초기화됩니다.";
            await dialog.ShowAsync();
        }
    }
}
