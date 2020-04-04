using MoneyNoteLibrary.Models;
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

// 빈 페이지 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=234238에 나와 있습니다.

namespace MoneyNote.Views
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    public sealed partial class MoneyBasicListPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private MoneyViewModel _ViewModel;
        public MoneyViewModel ViewModel
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

        private BankBookViewModel _BankBookViewModel;
        public BankBookViewModel BankBookViewModel
        {
            get { return _BankBookViewModel; }
            set
            {
                if (_BankBookViewModel == value)
                    return;

                _BankBookViewModel = value;
                OnPropertyChanged();
            }
        }

        public MoneyBasicListPage()
        {
            this.InitializeComponent();
            this.Loaded += MoneyBasicList_Loaded;
            this.Unloaded += MoneyBasicList_Unloaded;
        }

        private void MoneyBasicList_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new MoneyViewModel(App.LogInedUser);
            BankBookViewModel = new BankBookViewModel(App.LogInedUser);
        }

        private void MoneyBasicList_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void MoneyListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView)
            {
                var selectedItem = listView.SelectedItem;
                if (selectedItem is MoneyItem item)
                {
                    //HomePage.CurrentHomePage.MenuContent.Navigate(typeof(MoneyDetailView), item);
                }
            }
        }

        private void NavigateCreatePage_Click(object sender, RoutedEventArgs e)
        {
            HomePage.CurrentHomePage.MenuContent.Navigate(typeof(MoneyCreateView));
        }
    }
}
