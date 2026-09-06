using ChillChill.Contract.Auth;
using ChillChill.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace ChillChill.ViewModels
{
    public partial class RegisterViewModel : ViewModelBase
    {
        private readonly Action _goToLogin;
        private readonly IApiClient _apiClient;
        public RegisterViewModel(Action goToLogin, IApiClient apiClient)
        {
            _goToLogin = goToLogin;
            _apiClient = apiClient;
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

        [ObservableProperty]
        private bool _isLoading = false;

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        [RelayCommand]
        private async Task RegisterAsync()
        {
            try
            {
                IsLoading = true;
                if (Password != ConfirmPassword)
                {
                    ErrorMessage = "Passwords do not match.";
                    return;
                }

                var result = await _apiClient.RegisterAsync(new RegisterRequest
                {
                    Username = Username,
                    Password = Password,
                    DisplayName = DisplayName
                });
                Console.WriteLine(result);
                if (result.IsSuccess == false)
                {
                    ErrorMessage = result.ErrorMessage;
                    return;
                }
                ErrorMessage = string.Empty;

                _goToLogin();
            }
            catch (Exception ex) { }
            finally
            {
                IsLoading = false;
            }
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
