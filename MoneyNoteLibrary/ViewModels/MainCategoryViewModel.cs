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
using static MoneyNoteLibrary.Enums.MoneyEnum;

namespace MoneyNoteLibrary.ViewModels
{
    public class MainCategoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private MoneyCategory _Division;
        public MoneyCategory Division
        {
            get { return _Division; }
            set
            {
                if (_Division == value)
                    return;

                _Division = value;
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

        public User LoginedUser { get; set; }

        private string _AddCategoryButtonText = "새 분류 추가";
        public string AddCategoryButtonText
        {
            get { return _AddCategoryButtonText; }
            set
            {
                if (_AddCategoryButtonText == value)
                    return;

                _AddCategoryButtonText = value;
                OnPropertyChanged();
            }
        }


        private string _SaveButtonText = "저장";
        public string SaveButtonText
        {
            get { return _SaveButtonText; }
            set
            {
                if (_SaveButtonText == value)
                    return;

                _SaveButtonText = value;
                OnPropertyChanged();
            }
        }

        private bool _IsShowSubCategory;
        public bool IsShowSubCategory
        {
            get { return _IsShowSubCategory; }
            set
            {
                if (_IsShowSubCategory == value)
                    return;

                _IsShowSubCategory = value;
                OnPropertyChanged();
                if (_IsShowSubCategory) SetUpdateMode();
                ValidCheck();
            }
        }

        private bool _IsShowAddNewCategory;
        public bool IsShowAddNewCategory
        {
            get { return _IsShowAddNewCategory; }
            set
            {
                if (_IsShowAddNewCategory == value)
                    return;

                _IsShowAddNewCategory = value;

                OnPropertyChanged();
                if (_IsShowAddNewCategory) SetNewSaveMode();
                ValidCheck();
            }
        }

        private string _CategoryText;
        public string CategoryText
        {
            get { return _CategoryText; }
            set
            {
                if (_CategoryText == value)
                    return;

                _CategoryText = value;
                OnPropertyChanged();
                ValidCheck();
            }
        }

        public string SelectedCategoryTitle => SelectedCategory != null ? SelectedCategory.Title : string.Empty;

        private MainCategory _SelectedCategory;
        public MainCategory SelectedCategory
        {
            get { return _SelectedCategory; }
            set
            {
                if (_SelectedCategory == value)
                    return;

                _SelectedCategory = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(SelectedCategoryTitle));
            }
        }

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

        public bool IsValidCategoryText => IsShowAddNewCategory && !string.IsNullOrEmpty(CategoryText);

        public bool IsChangedCategory => false;

        public bool IsSaveButtonEnabled => IsValidCategoryText || IsChangedCategory;

        public MainCategoryViewModel(User user, MoneyCategory div)
        {
            LoginedUser = user;
            Division = div;
            Initialize();
        }

        public async void Initialize()
        {
            MainCategories = new ObservableCollection<MainCategory>();
            SubCategories = new ObservableCollection<SubCategory>();

            // 수입 지출 구분에 따른 해당 메인 카테고리들 가져오기
            var result = await MoneyApi.GetMainCategories.ApiLauncher<User, List<MainCategory>>(LoginedUser, ControllerEnum.category);
            if (result.Result)
            {
                foreach (var item in result.Content)
                {
                    if (item.Division == Division)
                        MainCategories.Add(item);
                }
            }
        }

        public async Task GetSubCategory(MainCategory selectedCategory)
        {
            SubCategories = new ObservableCollection<SubCategory>();
            var result = await MoneyApi.GetSubCategories.ApiLauncher<MainCategory, List<SubCategory>>(selectedCategory, ControllerEnum.category);
            if (result.Result)
            {
                foreach (var item in result.Content)
                {
                    if (item.Division == Division)
                        SubCategories.Add(item);
                }
            }
        }

        public void ValidCheck()
        {
            OnPropertyChanged(nameof(IsValidCategoryText));
            OnPropertyChanged(nameof(IsChangedCategory));
            OnPropertyChanged(nameof(IsSaveButtonEnabled));
        }

        public async Task<bool> SaveCategory()
        {
            if (string.IsNullOrEmpty(CategoryText))
                return false;

            if (LoginedUser == null)
                return false;

            var category = new MainCategory()
            {
                Division = Division,
                Title = CategoryText,
                User = LoginedUser,
            };

            var result = await MoneyApi.SaveMainCategory.ApiLauncher<MainCategory, MainCategory>(category, ControllerEnum.category);
            if (!result.Result)
                ErrorMessage = "에러가 발생했습니다.";
            else
            {
                if (IsShowAddNewCategory) IsShowAddNewCategory = false;
                CategoryText = string.Empty;
                MainCategories.Add(result.Content);
            }
            return result.Result;
        }

        public async Task<bool> SaveSubCategory()
        {
            if (string.IsNullOrEmpty(CategoryText))
                return false;

            if (LoginedUser == null)
                return false;

            if (SelectedCategory == null)
                return false;

            var category = new SubCategory()
            {
                Division = Division,
                Title = CategoryText,
                MainCategoryId = SelectedCategory.Id
            };

            var result = await MoneyApi.SaveSubCategory.ApiLauncher<SubCategory, SubCategory>(category, ControllerEnum.category);
            if (!result.Result)
                ErrorMessage = "에러가 발생했습니다.";
            else
            {
                if (IsShowAddNewCategory) IsShowAddNewCategory = false;
                CategoryText = string.Empty;
                SubCategories.Add(result.Content);
            }
            return result.Result;
        }

        public void SetNewSaveMode()
        {
            //IsShowSubCategory = false;
            CategoryText = string.Empty;
            SaveButtonText = "새로운 카테고리 추가";
            if (SelectedCategory == null) AddCategoryButtonText = "새 분류 추가";
            OnPropertyChanged(nameof(SaveButtonText));
        }

        public void SetUpdateMode()
        {
            IsShowAddNewCategory = false;
            CategoryText = string.Empty;
            SaveButtonText = "수정된 항목 저장";
            AddCategoryButtonText = "하위 분류 추가";
            OnPropertyChanged(nameof(SaveButtonText));
        }

        public void CancelSelectedCategory()
        {
            SelectedCategory = null;
        }
    }
}
