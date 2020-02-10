using MoneyNoteLibrary.Common;
using MoneyNoteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static MoneyNoteLibrary.Enums.MoneyApiInfo;

namespace MoneyNoteLibrary.ViewModels
{
    public class MoneyHandleViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _IsRunProgressRing;
        public bool IsRunProgressRing
        {
            get { return _IsRunProgressRing; }
            set
            {
                if (_IsRunProgressRing == value)
                    return;

                _IsRunProgressRing = value;
                OnPropertyChanged();
            }
        }

        private bool _IsMainCategoryProgress;
        public bool IsMainCategoryProgress
        {
            get { return _IsMainCategoryProgress; }
            set
            {
                if (_IsMainCategoryProgress == value)
                    return;

                _IsMainCategoryProgress = value;
                OnPropertyChanged();
            }
        }

        private bool _IsSubCategoryProgress;
        public bool IsSubCategoryProgress
        {
            get { return _IsSubCategoryProgress; }
            set
            {
                if (_IsSubCategoryProgress == value)
                    return;

                _IsSubCategoryProgress = value;
                OnPropertyChanged();
            }
        }

        private MoneyItem _PreMoneyItem;
        public MoneyItem PreMoneyItem
        {
            get { return _PreMoneyItem; }
            set
            {
                if (_PreMoneyItem == value)
                    return;

                _PreMoneyItem = value;
                OnPropertyChanged();
            }
        }

        public User LoginedUser { get; set; }

        private ObservableCollection<MainCategory> _MainCategories;
        public ObservableCollection<MainCategory> MainCategories
        {
            get { return _MainCategories; }
            set
            {
                if (_MainCategories == value)
                    return;

                _MainCategories = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<SubCategory> _SubCategories;
        public ObservableCollection<SubCategory> SubCategories
        {
            get { return _SubCategories; }
            set
            {
                if (_SubCategories == value)
                    return;

                _SubCategories = value;
                OnPropertyChanged();
            }
        }

        private string _ErrorMessage;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set
            {
                if (_ErrorMessage == value)
                    return;

                _ErrorMessage = value;
                OnPropertyChanged();
            }
        }

        private DateTimeOffset _CreatedTime = DateTimeOffset.Now;
        public DateTimeOffset CreatedTime
        {
            get { return _CreatedTime; }
            set
            {
                if (_CreatedTime == value)
                    return;

                _CreatedTime = value;
                OnPropertyChanged();
                ValidCheck();
            }
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                if (_Title == value)
                    return;

                _Title = value;
                OnPropertyChanged();
                ValidCheck();
            }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description == value)
                    return;

                _Description = value;
                OnPropertyChanged();
                ValidCheck();
            }
        }

        private string _MoneyText;
        public string MoneyText
        {
            get { return _MoneyText; }
            set
            {
                if (_MoneyText == value)
                    return;

                _MoneyText = value;
                OnPropertyChanged();
                ValidCheck();
            }
        }

        private bool _IsExpense = true;
        public bool IsExpense
        {
            get { return _IsExpense; }
            set
            {
                if (_IsExpense == value)
                    return;

                _IsExpense = value;
                OnPropertyChanged();
                ValidCheck();

                if (value)
                    CategoryInitialize();
            }
        }

        private bool _IsIncome = false;
        public bool IsIncome
        {
            get { return _IsIncome; }
            set
            {
                if (_IsIncome == value)
                    return;

                _IsIncome = value;
                OnPropertyChanged();

                if (value)
                    CategoryInitialize();
            }
        }

        private MainCategory _MainCategory;
        public MainCategory MainCategory
        {
            get { return _MainCategory; }
            set
            {
                if (_MainCategory == value)
                    return;

                _MainCategory = value;
                OnPropertyChanged();
            }
        }

        private SubCategory _SubCategory;
        public SubCategory SubCategory
        {
            get { return _SubCategory; }
            set
            {
                if (_SubCategory == value)
                    return;

                _SubCategory = value;
                OnPropertyChanged();
            }
        }

        public bool IsValidMoney => Common.ValidCheck.IsValidNumber(MoneyText);

        public bool IsValidDivision => IsExpense != IsIncome;

        public bool IsEnableSave => IsValidMoney && !string.IsNullOrEmpty(Title) && IsValidDivision;

        public MoneyHandleViewModel(User user)
        {
            LoginedUser = user;
            CategoryInitialize();
        }

        public MoneyHandleViewModel(User user, MoneyItem item)
        {
            LoginedUser = user;

            CategoryInitialize();
            if (item != null)
                SetViewModel(item);
        }

        public async void CategoryInitialize()
        {
            MainCategories = new ObservableCollection<MainCategory>();
            SubCategories = new ObservableCollection<SubCategory>();

            await GetMainCategories();
        }

        public void SetViewModel(MoneyItem item)
        {
            PreMoneyItem = item;

            Title = item.Title;
            Description = item.Description;
            MoneyText = item.Money.ToString();
            CreatedTime = item.CreatedTime;
            switch (item.Division)
            {
                case Enums.MoneyEnum.MoneyCategory.Expense:
                    IsIncome = false;
                    IsExpense = true;
                    break;
                case Enums.MoneyEnum.MoneyCategory.Income:
                    IsIncome = true;
                    IsExpense = false;
                    break;
                default:
                    break;
            }
            MainCategory = item.MainCategory;
            SubCategory = item.SubCategory;
        }

        public void ValidCheck()
        {
            OnPropertyChanged(nameof(IsValidMoney));
            OnPropertyChanged(nameof(IsValidDivision));
            OnPropertyChanged(nameof(IsEnableSave));
        }

        public async Task<bool> SaveMoney()
        {
            if (LoginedUser == null)
                return false;

            double.TryParse(MoneyText, out double mo);

            var item = new MoneyItem()
            {
                Title = Title,
                Description = Description,
                Money = mo,
                Division = IsIncome ? Enums.MoneyEnum.MoneyCategory.Income : Enums.MoneyEnum.MoneyCategory.Expense,
                MainCategory = MainCategory,
                
                User = LoginedUser
            };
            //SubCategory = SubCategory,
            var result = await MoneyApi.SaveMoney.ApiLauncher<MoneyItem, MoneyItem>(item);

            if (!result.Result)
                ErrorMessage = "에러가 발생했습니다.";

            return result.Result;
        }

        public async Task<bool> ModifyMoney()
        {
            if (LoginedUser == null)
                return false;

            double.TryParse(MoneyText, out double mo);
            PreMoneyItem.Title = Title;
            PreMoneyItem.Description = Description;
            PreMoneyItem.Money = mo;
            PreMoneyItem.Division = IsIncome ? Enums.MoneyEnum.MoneyCategory.Income : Enums.MoneyEnum.MoneyCategory.Expense;
            PreMoneyItem.MainCategory = MainCategory;
            PreMoneyItem.SubCategory = SubCategory;
            PreMoneyItem.User = LoginedUser;

            var result = await MoneyApi.UpdateMoney.ApiLauncher<MoneyItem, MoneyItem>(PreMoneyItem);

            if (!result.Result)
                ErrorMessage = "에러가 발생했습니다.";

            return result.Result;
        }

        public async Task GetMainCategories()
        {
            if (LoginedUser == null)
                return;

            IsRunProgressRing = true;
            IsMainCategoryProgress = true;
            var result = await MoneyApi.GetMainCategories.ApiLauncher<User, List<MainCategory>>(LoginedUser, ControllerEnum.category);
            if (result.Result)
            {
                foreach (var item in result.Content)
                {
                    var nowDivision = IsIncome ? Enums.MoneyEnum.MoneyCategory.Income : Enums.MoneyEnum.MoneyCategory.Expense;
                    if (item.Division == nowDivision)
                        MainCategories.Add(item);
                }
            }

            IsMainCategoryProgress = false;
            IsRunProgressRing = false;
        }

        public async Task GetSubCategories()
        {
            if (MainCategory == null)
                return;

            if (LoginedUser == null)
                return;

            IsSubCategoryProgress = true;
            IsRunProgressRing = true;
            var result = await MoneyApi.GetSubCategories.ApiLauncher<MainCategory, ObservableCollection<SubCategory>>(MainCategory, ControllerEnum.category);
            if (result.Result)
            {
                foreach (var item in result.Content)
                {
                    var nowDivision = IsIncome ? Enums.MoneyEnum.MoneyCategory.Income : Enums.MoneyEnum.MoneyCategory.Expense;
                    if (item.Division == nowDivision)
                        SubCategories.Add(item);
                }
            }
            IsRunProgressRing = false;
            IsSubCategoryProgress = false;
        }
    }
}
