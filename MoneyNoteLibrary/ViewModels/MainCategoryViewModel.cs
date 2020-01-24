using MoneyNoteLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
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
                OnPropertyChanged(nameof(SaveButtonText));
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

                if (value)
                    SaveButtonText = "새로운 카테고리 추가";
                else
                    SaveButtonText = "저장";

                OnPropertyChanged();
                OnPropertyChanged(nameof(SaveButtonText));
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
            }
        }

        private List<MainCategory> _MainCategories;
        public List<MainCategory> MainCategories
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

        private List<SubCategory> _SubCategories;
        public List<SubCategory> SubCategories
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

        public MainCategoryViewModel(MoneyCategory div)
        {
            Division = div;
        }

        public void Intialize()
        {
            // 수입 지출 구분에 따른 해당 메인 카테고리들 가져오기
        }

        public void SaveCategory()
        {
            var category = new MainCategory()
            {
                Title = CategoryText
            };

            // 저장
        }

        public void SetSaveButton()
        {
            if (IsShowSubCategory)
            {
            }
            else if (IsShowAddNewCategory)
            {
            }
        }
    }
}
