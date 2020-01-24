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
using static MoneyNoteLibrary.Enums.MoneyEnum;

// 사용자 정의 컨트롤 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=234236에 나와 있습니다.

namespace MoneyNote.UserControls
{
    public sealed partial class MainCategorySetting : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private MainCategoryViewModel _ViewModel;
        public MainCategoryViewModel ViewModel
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

        public static readonly DependencyProperty DivisionProperty =
            DependencyProperty.Register("Division", typeof(MoneyCategory), typeof(MainCategorySetting), new PropertyMetadata(default(MoneyCategory)));

        public MoneyCategory Division
        {
            get { return (MoneyCategory)GetValue(DivisionProperty); }
            set { SetValue(DivisionProperty, value); }
        }

        public MainCategorySetting()
        {
            this.InitializeComponent();
            this.Loaded += MainCategorySetting_Loaded;
            this.Unloaded += MainCategorySetting_Unloaded;
        }

        private void MainCategorySetting_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new MainCategoryViewModel(App.LogInedUser, Division);
        }

        private void MainCategorySetting_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void MainCategoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainCategoryListView.SelectedItem != null)
            {
                ViewModel.IsShowSubCategory = true;
                if (MainCategoryListView.SelectedItem is MainCategory category)
                {
                    ViewModel.SelectedCategory = category;
                }
            }
        }

        private async void SaveCategory_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.SaveCategory();
        }
    }
}
