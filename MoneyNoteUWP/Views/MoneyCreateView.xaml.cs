﻿using MoneyNoteLibrary.ViewModels;
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
    public sealed partial class MoneyCreateView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        public MoneyCreateView()
        {
            this.InitializeComponent();
            this.Loaded += MoneyCreateView_Loaded;
            this.Unloaded += MoneyCreateView_Unloaded;
        }

        private void MoneyCreateView_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new MoneyHandleViewModel(App.LogInedUser);
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void MoneyCreateView_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private async void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.MainCategory):
                    if (ViewModel.MainCategory != null)
                        await ViewModel.GetSubCategories();
                    break;
                default:
                    break;
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await ViewModel.SaveMoney();
            if (result)
                HomePage.CurrentHomePage.MenuContent.Navigate(typeof(MoneyBasicListPage));
        }
    }
}
