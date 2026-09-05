using ChillChill.Contract.Auth;
using ChillChill.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace ChillChill.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly Action _goToRegister;
        private readonly Action _goToDashboard;
        private readonly IApiClient _apiClient;

        public LoginViewModel(Action goToRegister, Action goToDashboard, IApiClient apiClient)
        {
            _goToRegister = goToRegister;
            _goToDashboard = goToDashboard;
            _apiClient = apiClient;
        }

        [ObservableProperty]
        private string _username = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        [RelayCommand]
        private async Task LoginAsync()
        {
            var result = await _apiClient.LoginAsync(new LoginRequest
            {
                Username = Username,
                Password = Password
            });
            if (result is null)
            {
                ErrorMessage = "Invalid username or password.";
                return;
            }

            ErrorMessage = string.Empty;

            _goToDashboard();
        }

        [RelayCommand]
        private void Register()
        {
            _goToRegister();
        }

        partial void OnErrorMessageChanged(string value)
        {
            OnPropertyChanged(nameof(HasError));
        }
    }
}
