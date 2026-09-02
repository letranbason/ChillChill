using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace ChillChill.ViewModels
{
    public partial class RegisterViewModel : ViewModelBase
    {
        private readonly Action _goToLogin;

        public RegisterViewModel(Action goToLogin)
        {
            _goToLogin = goToLogin;
        }

        [ObservableProperty]
        private string _username = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        [ObservableProperty]
        private string _confirmPassword = string.Empty;

        [ObservableProperty]
        private string _displayName = string.Empty;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        [RelayCommand]
        private async Task RegisterAsync()
        {
            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Passwords do not match.";
                return;
            }

            await Task.CompletedTask;
        }

        [RelayCommand]
        private void Login()
        {
            _goToLogin();
        }

        partial void OnErrorMessageChanged(string value)
        {
            OnPropertyChanged(nameof(HasError));
        }
    }
}
