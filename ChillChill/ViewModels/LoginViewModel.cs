using ChillChill.Contract.Auth;
using ChillChill.Services;
using ChillChill.Services.Auth;
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
        private readonly IAuthSession _authSession;

        public LoginViewModel(Action goToRegister, Action goToDashboard, IApiClient apiClient, IAuthSession authSession)
        {
            _goToRegister = goToRegister;
            _goToDashboard = goToDashboard;
            _apiClient = apiClient;
            _authSession = authSession;
        }

        [ObservableProperty]
        private string _username = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        [ObservableProperty]
        private string _errorMessage = string.Empty;
        [ObservableProperty]
        private bool _isLoading = false;

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        [RelayCommand]
        private async Task LoginAsync()
        {
            try
            {
                IsLoading = true;
                var result = await _apiClient.LoginAsync(new LoginRequest
                {
                    Username = Username,
                    Password = Password
                });
                if (string.IsNullOrEmpty(result.Token) || result is null || result.User is null)
                {
                    ErrorMessage = "Invalid username or password.";
                    return;
                }

                ErrorMessage = string.Empty;

                _authSession.Token = result.Token;
                _authSession.User = result.User;
                _goToDashboard();
            }

            catch (Exception ex) { 
            }

            finally { IsLoading = false; }
            
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
