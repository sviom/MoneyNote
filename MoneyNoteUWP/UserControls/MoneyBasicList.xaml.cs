using MoneyNote.Views;
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

// 사용자 정의 컨트롤 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=234236에 나와 있습니다.

namespace MoneyNote.UserControls
{
    public sealed partial class MoneyBasicList : UserControl, INotifyPropertyChanged
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

        public MoneyBasicList()
        {
            this.InitializeComponent();
            this.Loaded += MoneyBasicList_Loaded;
            this.Unloaded += MoneyBasicList_Unloaded;
        }

        private void MoneyBasicList_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new MoneyViewModel();
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
                    HomePage.CurrentHomePage.Frame.Navigate(typeof(MoneyDetailView), item);
                }
            }
        }

        private void NavigateCreatePage_Click(object sender, RoutedEventArgs e)
        {
            HomePage.CurrentHomePage.Frame.Navigate(typeof(MoneyCreateView));
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
