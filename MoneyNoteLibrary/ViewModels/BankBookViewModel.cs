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

        public bool IsValidAssets => Common.ValidCheck.IsValidNumber(AssetsText);

        public bool IsEnableSave => IsValidAssets && !string.IsNullOrEmpty(Name);

        public BankBookViewModel(User user)
        {
            LoginedUser = user;
            Initialize();
        }

        public void Initialize()
        {
            BankBooks = new ObservableCollection<BankBook>();
        }

        public void ValidCheck()
        {
            OnPropertyChanged(nameof(IsValidAssets));
            OnPropertyChanged(nameof(IsEnableSave));
        }

        public void ShowInputArea()
        {
            IsShowInputArea = true;
        }

        public void HideInputArea()
        {
            IsShowInputArea = false;
        }

        public async Task<bool> SaveBankBook()
        {
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
                HideInputArea();
            }

            return result.Result;
        }
    }
}
