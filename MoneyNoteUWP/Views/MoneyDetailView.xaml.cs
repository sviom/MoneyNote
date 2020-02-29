using MoneyNoteLibrary.Models;
using MoneyNoteLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

        public MoneyDetailView()
        {
            this.InitializeComponent();
            this.Loaded += MoneyDetailView_Loaded;
            this.Unloaded += MoneyDetailView_Unloaded;
        }

        private void MoneyDetailView_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new MoneyHandleViewModel(App.LogInedUser, MoneyItem);
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            BankBookViewModel = new BankBookViewModel(App.LogInedUser);
            BankBookViewModel.PropertyChanged += BankBookViewModel_PropertyChanged;
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

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ViewModel.IsMainCategoryProgress):
                    if (!ViewModel.IsMainCategoryProgress)
                        SetMainCategoryCombobox(MoneyItem.MainCategory);
                    break;
                case nameof(ViewModel.IsSubCategoryProgress):
                    if (!ViewModel.IsSubCategoryProgress)
                        SetSubCategoryCombobox(MoneyItem.SubCategory);
                    break;
                default:
                    break;
            }
        }

        private void BankBookViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(BankBookViewModel.IsBankBooksProgress):
                    if (!BankBookViewModel.IsBankBooksProgress)
                    {
                        SetBankBookCombobox(MoneyItem.BankBook);
                    }
                    break;
                default:
                    break;
            }
        }

        private async void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await ViewModel.ModifyMoney();
            if (result)
                HomePage.CurrentHomePage.MenuContent.Navigate(typeof(MoneyBasicListPage));
        }

        public void SetMainCategoryCombobox(MainCategory mainCategory)
        {
            if (mainCategory == null)
                return;

            foreach (var item in MainCategoryCombobox.Items)
            {
                if (item is MainCategory category)
                {
                    if (category.Id == mainCategory.Id)
                        MainCategoryCombobox.SelectedItem = item;
                }
            }
        }

        public void SetSubCategoryCombobox(SubCategory subCategory)
        {
            if (subCategory == null)
                return;

            foreach (var item in SubCategoryCombobox.Items)
            {
                if (item is SubCategory category)
                {
                    if (category.Id == subCategory.Id)
                        SubCategoryCombobox.SelectedItem = item;
                }
            }
        }

        public void SetBankBookCombobox(BankBook bankBook)
        {
            if (bankBook == null)
                return;

            foreach (var item in BankbookCombobox.Items)
            {
                if (item is BankBook book)
                {
                    if (book.Id == bankBook.Id)
                        BankbookCombobox.SelectedItem = item;
                }
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await ViewModel.DeleteMoney();
            if (result)
                HomePage.CurrentHomePage.MenuContent.Navigate(typeof(MoneyBasicListPage));
        }
    }
}
