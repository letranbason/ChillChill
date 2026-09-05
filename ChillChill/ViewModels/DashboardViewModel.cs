using CommunityToolkit.Mvvm.Input;
using System;

namespace ChillChill.ViewModels
{
    public partial class DashboardViewModel : ViewModelBase
    {
        private readonly Action _goToLogin;
        public DashboardViewModel(Action goToLogin)
        {
            _goToLogin = goToLogin;
        }
        [RelayCommand]
        public void Logout()
        {
            _goToLogin();
        }
    }
}
