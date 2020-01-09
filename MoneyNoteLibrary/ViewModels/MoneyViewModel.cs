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
                OnPropertyChanged(nameof(MoneySum));
            }
        }

        public double MoneySum
        {
            get
            {
                double result = 0;
                foreach (var item in MoneyList)
                {
                    result += item.Money;
                }
                return result;
            }
        }

        public MoneyViewModel()
        {
            Initialize();
        }

        public async void Initialize()
        {
            MoneyList = new ObservableCollection<MoneyItem>();
            var result = await HttpLauncher.GetAll<User, MoneyItem>(new User() { Id = Guid.NewGuid(), Name = "test" });
            foreach (var item in result)
            {
                MoneyList.Add(item);
            }
            OnPropertyChanged(nameof(MoneySum));
        }
    }
}
