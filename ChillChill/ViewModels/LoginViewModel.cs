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
        private readonly IApiClient _apiClient;

        public LoginViewModel(Action goToRegister, IApiClient apiClient)
        {
            _goToRegister = goToRegister;
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
            var resutt = await _apiClient.LoginAsync(new LoginRequest
            {
                Username = Username,
                Password = Password
            });
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
