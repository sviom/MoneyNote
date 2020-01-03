using MoneyNoteLibrary.Common;
using MoneyNoteLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MoneyNoteLibrary.ViewModels
{
    public class MoneyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            }
        }

        private DateTimeOffset _CreatedTime;
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

        public bool IsValidMoney => Common.ValidCheck.IsValidNumber(MoneyText);

        public bool IsEnableSave => IsValidMoney && !string.IsNullOrEmpty(Title);

        public MoneyViewModel()
        {
            Initialize();
        }

        public MoneyViewModel(MoneyItem item)
        {
            Initialize();
            SetViewModel(item);
        }

        public async void Initialize()
        {
            MoneyList = new ObservableCollection<MoneyItem>();
            var result = await HttpLauncher.GetAll<User, MoneyItem>(new User() { Id = Guid.NewGuid(), Name = "test" });
            foreach (var item in result)
            {
                MoneyList.Add(item);
            }
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
                Money = mo
            };

            await HttpLauncher.Insert(item);
        }

        public void ModifyMoney()
        {

        }
    }
}
