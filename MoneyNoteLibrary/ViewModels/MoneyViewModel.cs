using MoneyNoteLibrary.Common;
using MoneyNoteLibrary.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static MoneyNoteLibrary.Enums.MoneyApiInfo;

namespace MoneyNoteLibrary.ViewModels
{
    public class MoneyItemsGroup : IGrouping<DateTime, MoneyItem>
    {
        public List<MoneyItem> _MoneyItems { get; set; }

        public MoneyItemsGroup(DateTime key, IEnumerable<MoneyItem> moneyItems)
        {
            Key = key;
            _MoneyItems = moneyItems.ToList();
        }

        public DateTime Key { get; }

        public string KeyHeader => Key.ToString("yyyy년 MM월 dd일");

        public IEnumerator<MoneyItem> GetEnumerator() => _MoneyItems.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _MoneyItems.GetEnumerator();
    }

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

        private List<MoneyItemsGroup> _MoneyGroupList;
        public List<MoneyItemsGroup> MoneyGroupList
        {
            get { return _MoneyGroupList; }
            set
            {
                if (_MoneyGroupList == value)
                    return;

                _MoneyGroupList = value;
                OnPropertyChanged();
            }
        }

        private DateTimeOffset _SelectedDate = DateTimeOffset.Now;
        public DateTimeOffset SelectedDate
        {
            get { return _SelectedDate; }
            set
            {
                if (_SelectedDate == value)
                    return;

                _SelectedDate = value;
                OnPropertyChanged();
                GetMoneyList(_SelectedDate);
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
            //Initialize();
            GetMoneyList(DateTimeOffset.Now);
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

                MoneyGroupList = MoneyList.GroupBy(x => x.CreatedTime.Date, (key, itemList) => new MoneyItemsGroup(key, itemList)).ToList();

                ReCalculate();
            }

            IsRunProgressRing = false;
        }

        public async void GetMoneyList(DateTimeOffset selectedDate)
        {
            IsRunProgressRing = true;
            MoneyList = new ObservableCollection<MoneyItem>();

            var reqeust = new ApiRequest<User, DateTimeOffset>(LoginedUser, selectedDate);

            //var encryptedId = UtilityLauncher.EncryptAES256(LoginedUser.Id.ToString(), AzureKeyVault.SaltPassword);
            var result = await MoneyApi.GetMoneyListWithDate.ApiLauncher<List<MoneyItem>>(reqeust);
            if (result.Result)
            {
                foreach (var item in result.Content)
                {
                    MoneyList.Add(item);
                }

                MoneyGroupList = MoneyList.GroupBy(x => x.CreatedTime.Date, (key, itemList) => new MoneyItemsGroup(key, itemList)).ToList();

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
