using MoneyNoteLibrary.Common;
using MoneyNoteLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MoneyNoteLibrary.ViewModels
{
    public class MoneyHandleViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
                ValidCheck();
            }
        }

        public bool IsValidMoney => Common.ValidCheck.IsValidNumber(MoneyText);

        public bool IsValidDivision => IsExpense != IsIncome;

        public bool IsEnableSave => IsValidMoney && !string.IsNullOrEmpty(Title) && IsValidDivision;

        public MoneyHandleViewModel() { }

        public MoneyHandleViewModel(MoneyItem item)
        {
            if (item != null)
                SetViewModel(item);
        }

        public void SetViewModel(MoneyItem item)
        {
            Title = item.Title;
            Description = item.Description;
            MoneyText = item.Money.ToString();
            CreatedTime = item.CreatedTime;
        }

        public void ValidCheck()
        {
            OnPropertyChanged(nameof(IsValidMoney));
            OnPropertyChanged(nameof(IsValidDivision));
            OnPropertyChanged(nameof(IsEnableSave));
        }

        public async Task SaveMoney()
        {
            double mo = 0;
            double.TryParse(MoneyText, out mo);

            var item = new MoneyItem()
            {
                Title = Title,
                Description = Description,
                Money = mo,
                Division = IsIncome ? Enums.MoneyEnum.MoneyCategory.Income : Enums.MoneyEnum.MoneyCategory.Expense
            };

            await HttpLauncher.Insert(item);
        }

        public void ModifyMoney()
        {

        }
    }
}
