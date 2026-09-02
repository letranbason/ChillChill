using ChillChill.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChillChill.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly IApiClient _apiClient;

        [ObservableProperty]
        private ViewModelBase _currentViewModel;

        public MainWindowViewModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
            CurrentViewModel = CreateLoginViewModel();
        }

        private LoginViewModel CreateLoginViewModel()
        {
            return new LoginViewModel(
                goToRegister: () =>
                {
                    CurrentViewModel = CreateRegisterViewModel();
                }, _apiClient);
        }
        private RegisterViewModel CreateRegisterViewModel()
        {
            return new RegisterViewModel(
                goToLogin: () =>
                {
                    CurrentViewModel = CreateLoginViewModel();
                });
        }
    }
}
