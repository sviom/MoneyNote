using MoneyNoteLibrary.Common;
using MoneyNoteLibrary.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 빈 페이지 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x412에 나와 있습니다.

namespace MoneyNote
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private LoginViewModel _ViewModel;
        public LoginViewModel ViewModel
        {
            get { return _ViewModel; }
            set
            {
                if (_ViewModel == value)
                    return;

                _ViewModel = value;
                OnPropertyChanged();
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
            this.Unloaded += MainPage_Unloaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new LoginViewModel();
            GetSavedId();
        }

        private void MainPage_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            await Login();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Views.SignUpPage));
        }

        private async void PasswordTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                await Login();
            }
        }

        public async Task Login()
        {
            (var result, var user) = await ViewModel.LogIn();
            if (result)
            {
                if (ViewModel.IsIdSaveChecked)
                    await SaveId();
                //AzureKeyVault.SaltPassword = await AzureKeyVault.OnGetAsync(KeyVaultName.SaltPassword.ToString());
                App.LogInedUser = user;
                Frame.Navigate(typeof(Views.HomePage));
            }
        }

        private async Task SaveId()
        {
            var idText = IdTextBox.Text;
            if (string.IsNullOrEmpty(idText))
                return;

            var storageFolder = ApplicationData.Current.LocalFolder;
            var sampleFile = await storageFolder.CreateFileAsync(LoginViewModel.SavedIdTextFile, CreationCollisionOption.ReplaceExisting);

            var saveForm = new SaveIdForm();
            saveForm.Id = idText;
            saveForm.IsSaveChecked = ViewModel.IsIdSaveChecked;
            var jsonText = JsonConvert.SerializeObject(saveForm);

            await FileIO.WriteTextAsync(sampleFile, jsonText);
        }

        public async void GetSavedId()
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            var sampleFile = await storageFolder.GetFileAsync(LoginViewModel.SavedIdTextFile);
            if (sampleFile != null)
            {
                string text = await FileIO.ReadTextAsync(sampleFile);
                var saveIdForm = JsonConvert.DeserializeObject<SaveIdForm>(text);
                if (saveIdForm.IsSaveChecked)
                {
                    IdTextBox.Text = saveIdForm.Id;
                }
            }

        }
    }
}
