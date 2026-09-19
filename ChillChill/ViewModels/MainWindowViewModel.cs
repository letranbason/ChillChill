using ChillChill.Services;
using ChillChill.Services.Auth;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChillChill.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly IApiClient _apiClient;
        private readonly IAuthSession _authSession;

        [ObservableProperty]
        private ViewModelBase _currentViewModel;

        public MainWindowViewModel(IApiClient apiClient, IAuthSession authSession)
        {
            _apiClient = apiClient;
            _authSession = authSession;
            CurrentViewModel = CreateLoginViewModel();
        }

        private LoginViewModel CreateLoginViewModel()
        {
            return new LoginViewModel(
                goToRegister: () =>
                {
                    CurrentViewModel = CreateRegisterViewModel();
                },
                goToDashboard: () =>
                {
                    CurrentViewModel = CreateDashboardViewModel();
                },
                _apiClient,
                _authSession);
        }
        private RegisterViewModel CreateRegisterViewModel()
        {
            return new RegisterViewModel(
                goToLogin: () =>
                {
                    CurrentViewModel = CreateLoginViewModel();
                },
                _apiClient);
        }

        private DashboardViewModel CreateDashboardViewModel()
        {
            return new DashboardViewModel(
                goToLogin: () =>
                {
                    CurrentViewModel = CreateLoginViewModel();
                });
        }
    }
}
