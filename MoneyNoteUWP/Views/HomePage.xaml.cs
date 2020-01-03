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

// 빈 페이지 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=234238에 나와 있습니다.

namespace MoneyNote.Views
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    public sealed partial class HomePage : Page, INotifyPropertyChanged
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

        public HomePage()
        {
            this.InitializeComponent();
            this.Loaded += HomePage_Loaded;
            this.Unloaded += HomePage_Unloaded;
        }

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new MoneyViewModel();
            ViewModel.MoneyList = test;
        }

        private void HomePage_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private ObservableCollection<MoneyItem> test = new ObservableCollection<MoneyItem>()
        {
            new MoneyItem(){ Title = "TEst", Money=1000000 },
            new MoneyItem(){ Title = "TEst", Money=1000000 },
            new MoneyItem(){ Title = "TEst", Money=1000000 },
        };

        private void NavigateCreatePage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MoneyCreateView));
        }

        private void MoneyListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView)
            {
                var selectedItem = listView.SelectedItem;
                if (selectedItem is MoneyItem item)
                {
                    this.Frame.Navigate(typeof(MoneyDetailView), item);
                }
            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
