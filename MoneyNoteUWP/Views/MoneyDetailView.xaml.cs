﻿using MoneyNoteLibrary.Models;
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
    public sealed partial class MoneyDetailView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private MoneyItem _MoneyItem;
        public MoneyItem MoneyItem
        {
            get { return _MoneyItem; }
            set
            {
                if (_MoneyItem == value)
                    return;

                _MoneyItem = value;
                OnPropertyChanged();
            }
        }

        private MoneyHandleViewModel _ViewModel;
        public MoneyHandleViewModel ViewModel
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

        public MoneyDetailView()
        {
            this.InitializeComponent();
            this.Loaded += MoneyDetailView_Loaded;
            this.Unloaded += MoneyDetailView_Unloaded;
        }

        private void MoneyDetailView_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new MoneyHandleViewModel(App.LogInedUser, MoneyItem);
        }

        private void MoneyDetailView_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is MoneyItem item)
            {
                MoneyItem = item;
            }
        }

        private async void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await ViewModel.ModifyMoney();
            if (result)
                HomePage.CurrentHomePage.MenuContent.Navigate(typeof(MoneyBasicListPage));
        }
    }
}
