using MoneyNoteLibrary5.Common;
using MoneyNoteLibrary5.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static MoneyNoteLibrary5.Enums.MoneyApiInfo;
using static MoneyNoteLibrary5.Enums.MoneyEnum;

namespace MoneyNoteLibrary5.ViewModels
{
    public class MainCategoryViewModel : ViewModelBase
    {
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

        private string _SubCategoryText;
        public string SubCategoryText
        {
            get { return _SubCategoryText; }
            set
            {
                if (_SubCategoryText == value)
                    return;

                _SubCategoryText = value;
                OnPropertyChanged();
                ValidCheck();
            }
        }

        private MainCategory _SelectedCategory = new MainCategory();
        public MainCategory SelectedCategory
        {
            get { return _SelectedCategory; }
            set
            {
                if (_SelectedCategory == value)
                    return;

                _SelectedCategory = value;

                if (_SelectedCategory != null) CategoryText = _SelectedCategory.Title;
                else CategoryText = string.Empty;

                OnPropertyChanged();
                ValidCheck();
            }
        }

        private SubCategory _SelectedSubCategory = new SubCategory();
        public SubCategory SelectedSubCategory
        {
            get { return _SelectedSubCategory; }
            set
            {
                if (_SelectedSubCategory == value)
                    return;

                _SelectedSubCategory = value;

                if (_SelectedSubCategory != null) SubCategoryText = _SelectedSubCategory.Title;
                else SubCategoryText = string.Empty;

                OnPropertyChanged();
                ValidCheck();
            }
        }

        private ObservableCollection<MainCategory> _MainCategories = new ObservableCollection<MainCategory>();
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

        private ObservableCollection<SubCategory> _SubCategories = new ObservableCollection<SubCategory>();
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

        public bool IsSaveButtonEnabled => !string.IsNullOrEmpty(CategoryText);

        public bool IsSubSaveButtonEnabled => !string.IsNullOrEmpty(SubCategoryText);

        public MainCategoryViewModel(User user, MoneyCategory div)
        {
            LoginedUser = user;
            Division = div;
            //Initialize();
        }

        public async Task Initialize()
        {
            MainCategories = new ObservableCollection<MainCategory>();
            SubCategories = new ObservableCollection<SubCategory>();

            IsRunProgressRing = true;
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
            IsRunProgressRing = false;
        }

        public async Task GetSubCategory(MainCategory mainCategory)
        {
            if (mainCategory == null)
                return;

            SelectedCategory = mainCategory;

            IsRunProgressRing = true;
            SubCategories = new ObservableCollection<SubCategory>();
            //var result = await MoneyApi.GetSubCategories.ApiLauncher<MainCategory, List<SubCategory>>(selectedCategory, ControllerEnum.category);
            var result = await MoneyApi.GetSubCategories.ApiGetLauncher<ObservableCollection<SubCategory>>($"guid={mainCategory.Id}", ControllerEnum.category);
            if (result.Result)
            {
                foreach (var item in result.Content)
                {
                    if (item.Division == Division)
                        SubCategories.Add(item);
                }
            }
            IsRunProgressRing = false;
        }

        public void ValidCheck()
        {
            OnPropertyChanged(nameof(IsSubSaveButtonEnabled));
            OnPropertyChanged(nameof(IsSaveButtonEnabled));
        }

        public async Task<bool> SaveCategoryWithText()
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

            return await SaveCategory(category);
        }

        public async Task<bool> SaveCategoryWithClass(MainCategory mainCategory)
        {
            if (mainCategory == null)
                return false;

            if (LoginedUser == null)
                return false;

            mainCategory.Division = Division;
            mainCategory.User = LoginedUser;

            return await SaveCategory(mainCategory);
        }

        private async Task<bool> SaveCategory(MainCategory mainCategory)
        {
            var result = await MoneyApi.SaveMainCategory.ApiLauncher<MainCategory, MainCategory>(mainCategory, ControllerEnum.category);

            if (!result.Result)
                ErrorMessage = "에러가 발생했습니다.";
            else
            {
                CategoryText = string.Empty;
                MainCategories.Add(result.Content);
            }
            return result.Result;
        }

        public async Task<bool> SaveSubCategory()
        {
            if (string.IsNullOrEmpty(SubCategoryText))
                return false;

            if (LoginedUser == null)
                return false;

            if (SelectedCategory == null || SelectedCategory.Id == Guid.Empty)
            {
                ErrorMessage = "부모는 반드시 선택해주세요.";
                return false;
            }

            var category = new SubCategory()
            {
                Division = Division,
                Title = SubCategoryText,
                MainCategoryId = SelectedCategory.Id
            };

            var result = await MoneyApi.SaveSubCategory.ApiLauncher<SubCategory, SubCategory>(category, ControllerEnum.category);

            if (!result.Result)
                ErrorMessage = "에러가 발생했습니다.";
            else
            {
                SubCategoryText = string.Empty;
                SubCategories.Add(result.Content);
            }
            return result.Result;
        }

        public void ClearSelectedCategory()
        {
            SelectedCategory = new MainCategory();
            IsShowSubCategory = false;
        }

        public void ClearSelectedSubCategory()
        {
            SelectedSubCategory = new SubCategory();
            SubCategoryText = string.Empty;
        }

        public async Task<bool> UpdateCategory(MainCategory updateCategory)
        {
            //if (string.IsNullOrEmpty(CategoryText))
            //    return false;

            if (LoginedUser == null)
                return false;

            if (SelectedCategory == null)
                return false;

            var mainCategory = SelectedCategory;

            //SelectedCategory.Title = CategoryText;

            var result = await MoneyApi.UpdateMainCategory.ApiLauncher<MainCategory, MainCategory>(SelectedCategory, ControllerEnum.category);

            if (!result.Result)
                ErrorMessage = "에러가 발생했습니다.";
            else
            {
                CategoryText = string.Empty;
                MainCategories.Remove(mainCategory);
                MainCategories.Add(result.Content);
                ClearSelectedCategory();
            }
            return result.Result;
        }

        public async Task<bool> UpdateSubCategory(SubCategory subCategory)
        {
            if (string.IsNullOrEmpty(SubCategoryText))
                return false;

            if (LoginedUser == null)
                return false;

            if (SelectedCategory == null)
            {
                ErrorMessage = "부모는 반드시 선택해주세요.";
                return false;
            }

            //var subCategory = new SubCategory()
            //{
            //    Division = Division,
            //    Title = SubCategoryText,
            //    MainCategoryId = SelectedCategory.Id
            //};

            subCategory.Id = SelectedSubCategory.Id;
            var result = await MoneyApi.UpdateSubCategory.ApiLauncher<SubCategory, SubCategory>(subCategory, ControllerEnum.category);

            if (!result.Result)
                ErrorMessage = "에러가 발생했습니다.";
            else
            {
                SubCategoryText = string.Empty;
                SubCategories.Remove(SelectedSubCategory);
                SubCategories.Add(result.Content);
                ClearSelectedSubCategory();
            }
            return result.Result;
        }

        public async Task DeleteCategory()
        {
            if (LoginedUser == null)
                return;

            if (SelectedCategory == null)
                return;

            IsRunProgressRing = true;
            var deleteResult = await MoneyApi.DeleteMainCategory.ApiLauncher<MainCategory, bool>(SelectedCategory, ControllerEnum.category);
            IsRunProgressRing = false;
            if (!deleteResult.Result)
            {
                ErrorMessage = "삭제 중 오류가 발생하였습니다.";
                return;
            }

            CategoryText = string.Empty;
            MainCategories.Remove(SelectedCategory);
            ClearSelectedCategory();
        }

        public async Task DeleteSubCategory()
        {
            if (LoginedUser == null)
                return;

            if (SelectedSubCategory == null)
                return;

            IsRunProgressRing = true;
            var deleteResult = await MoneyApi.DeleteSubCategory.ApiLauncher<SubCategory, bool>(SelectedSubCategory, ControllerEnum.category);
            IsRunProgressRing = false;
            if (!deleteResult.Result)
            {
                ErrorMessage = "삭제 중 오류가 발생하였습니다.";
                return;
            }

            SubCategoryText = string.Empty;
            SubCategories.Remove(SelectedSubCategory);
        }
    }
}
