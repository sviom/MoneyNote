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
    public class BankBookViewModel : ViewModelBase
    {

        private ObservableCollection<BankBook> _BankBooks;
        public ObservableCollection<BankBook> BankBooks
        {
            get { return _BankBooks; }
            set
            {
                if (_BankBooks == value)
                    return;

                _BankBooks = value;
                OnPropertyChanged();
            }
        }

        private BankBook _SelectedItem;
        public BankBook SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                if (_SelectedItem == value)
                    return;

                _SelectedItem = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(IsShowDeleteButton));
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name == value)
                    return;

                _Name = value;
                OnPropertyChanged();
                ValidCheck();
            }
        }

        private string _AssetsText;
        public string AssetsText
        {
            get { return _AssetsText; }
            set
            {
                if (_AssetsText == value)
                    return;

                _AssetsText = value;
                OnPropertyChanged();
                ValidCheck();
            }
        }

        private string _InputAreaText = "새로 추가";
        public string InputAreaText
        {
            get { return _InputAreaText; }
            set
            {
                if (_InputAreaText == value)
                    return;

                _InputAreaText = value;
                OnPropertyChanged();
            }
        }

        private bool _IsShowInputArea;
        public bool IsShowInputArea
        {
            get { return _IsShowInputArea; }
            set
            {
                if (_IsShowInputArea == value)
                    return;

                _IsShowInputArea = value;
                OnPropertyChanged();
            }
        }

        public bool IsValidAssets => Common.ValidCheck.IsValidNumber(AssetsText);

        public bool IsEnableSave => IsValidAssets && !string.IsNullOrEmpty(Name);

        public bool IsShowDeleteButton => SelectedItem != null;

        public BankBookViewModel(User user)
        {
            LoginedUser = user;
            Initialize();
        }

        public async void Initialize()
        {
            await GetBankBooks();
        }

        public void ValidCheck()
        {
            OnPropertyChanged(nameof(IsValidAssets));
            OnPropertyChanged(nameof(IsEnableSave));
        }

        public void SetSelectedItem(BankBook bankBook)
        {
            SelectedItem = bankBook;
            if (SelectedItem != null)
            {
                Name = SelectedItem.Name;
                AssetsText = SelectedItem.Assets.ToString();
            }
            ChangeInputArea();
        }

        public void ChangeInputArea()
        {
            IsShowInputArea = !IsShowInputArea;
            if (IsShowInputArea)
                InputAreaText = "내용 삭제";
            else
            {
                InputAreaText = "새로 추가";
                Clear();
            }
        }

        public void Clear()
        {
            Name = string.Empty;
            AssetsText = string.Empty;
            SelectedItem = null;
        }

        public async Task GetBankBooks()
        {
            IsRunProgressRing = true;
            BankBooks = new ObservableCollection<BankBook>();
            var result = await MoneyApi.GetBankBooks.ApiLauncher<User, List<BankBook>>(LoginedUser, ControllerEnum.bankbook);
            if (result.Result)
            {
                foreach (var item in result.Content)
                {
                    BankBooks.Add(item);
                }
            }
            else
            {
                ErrorMessage = "목록 가져오기에서 에러 발생했습니다.";
            }
            IsRunProgressRing = false;
        }

        public async Task<bool> SaveBankBook()
        {
            if (SelectedItem != null)
                return await ModifyBankBook();
            else
                return await SaveNewBankBook();
        }

        public async Task<bool> SaveNewBankBook()
        {
            IsRunProgressRing = true;

            double.TryParse(AssetsText, out double assets);
            var newBankBook = new BankBook();
            newBankBook.Name = Name;
            newBankBook.Assets = assets;
            newBankBook.User = LoginedUser;

            var result = await MoneyApi.SaveBankBook.ApiLauncher<BankBook, BankBook>(newBankBook, ControllerEnum.bankbook);
            if (!result.Result)
                ErrorMessage = "에러가 발생했습니다.";
            else
            {
                BankBooks.Add(result.Content);
                ChangeInputArea();
            }


            IsRunProgressRing = false;
            return result.Result;
        }

        public async Task<bool> ModifyBankBook()
        {
            if (SelectedItem == null)
                return false;

            IsRunProgressRing = true;

            double.TryParse(AssetsText, out double assets);

            SelectedItem.Name = Name;
            SelectedItem.Assets = assets;
            SelectedItem.User = LoginedUser;

            var result = await MoneyApi.ModifyBankBook.ApiLauncher<BankBook, BankBook>(SelectedItem, ControllerEnum.bankbook);
            if (!result.Result)
                ErrorMessage = "에러가 발생했습니다.";
            else
            {
                await GetBankBooks();
                ChangeInputArea();
            }

            IsRunProgressRing = false;
            return result.Result;
        }

        public async Task<bool> DeleteBankBook()
        {
            if (SelectedItem == null)
                return false;

            IsRunProgressRing = true;

            double.TryParse(AssetsText, out double assets);

            var result = await MoneyApi.DeleteBankBook.ApiLauncher<BankBook, bool>(SelectedItem, ControllerEnum.bankbook);
            if (!result.Result)
                ErrorMessage = "에러가 발생했습니다.";
            else
            {
                BankBooks.Remove(SelectedItem);
                ChangeInputArea();
            }

            IsRunProgressRing = false;
            return result.Result;
        }
    }
}
