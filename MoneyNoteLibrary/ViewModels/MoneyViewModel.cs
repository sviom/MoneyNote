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
    public class MoneyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public User LoginedUser { get; set; }

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

        private ObservableCollection<MoneyItem> _MoneyList;
        public ObservableCollection<MoneyItem> MoneyList
        {
            get { return _MoneyList; }
            set
            {
                if (_MoneyList == value)
                    return;

                _MoneyList = value;
                OnPropertyChanged();
                ReCalculate();
            }
        }

        public double MoneySum
        {
            get
            {
                double result = 0;
                foreach (var item in MoneyList)
                {
                    //result += item.Money;
                    if (item.Division == Enums.MoneyEnum.MoneyCategory.Expense)
                        result -= item.Money;
                    else
                        result += item.Money;
                }
                return result;
            }
        }

        public double IncomeSum
        {
            get
            {
                double result = 0;
                foreach (var item in MoneyList)
                {
                    if (item.Division == Enums.MoneyEnum.MoneyCategory.Income)
                        result += item.Money;
                }

                return result;
            }
        }

        public double ExpenseSum
        {
            get
            {
                double result = 0;
                foreach (var item in MoneyList)
                {
                    if (item.Division == Enums.MoneyEnum.MoneyCategory.Expense)
                        result += item.Money;
                }

                return result;
            }
        }

        public MoneyViewModel(User user)
        {
            LoginedUser = user;
            Initialize();
        }

        public async void Initialize()
        {
            IsRunProgressRing = true;
            MoneyList = new ObservableCollection<MoneyItem>();
            //var encryptedId = UtilityLauncher.EncryptAES256(LoginedUser.Id.ToString(), AzureKeyVault.SaltPassword);
            var result = await MoneyApi.GetAllMoney.ApiLauncher<string, List<MoneyItem>>(LoginedUser.Id.ToString());
            if (result.Result)
            {
                foreach (var item in result.Content)
                {
                    MoneyList.Add(item);
                }
                ReCalculate();
            }

            IsRunProgressRing = false;
        }

        public void ReCalculate()
        {
            OnPropertyChanged(nameof(MoneySum));
            OnPropertyChanged(nameof(IncomeSum));
            OnPropertyChanged(nameof(ExpenseSum));
        }
    }
}
