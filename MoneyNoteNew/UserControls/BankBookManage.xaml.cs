using MoneyNoteLibrary.Models;
using MoneyNoteLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MoneyNoteNew.UserControls
{
    public sealed partial class BankBookManage : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private BankBookViewModel _ViewModel;
        public BankBookViewModel ViewModel
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

        public BankBookManage()
        {
            this.InitializeComponent();
            this.Loaded += BankBookManage_Loaded;
            this.Unloaded += BankBookManage_Unloaded;
        }

        private void BankBookManage_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new BankBookViewModel(App.LogInedUser);
        }

        private void BankBookManage_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void BankBookListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var clickedItem = e.ClickedItem;
            if (clickedItem is BankBook bankBook)
            {
                ViewModel.SetSelectedItem(bankBook);
            }
        }
    }
}
