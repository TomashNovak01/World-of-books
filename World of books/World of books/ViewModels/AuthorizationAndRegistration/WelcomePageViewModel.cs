using World_of_books.ViewModels.Base;

namespace World_of_books.ViewModels.AuthorizationAndRegistration
{
    internal class WelcomePageViewModel : ViewModelBase
    {
        #region Fields

        #region UserName
        private string _userName;
        public string UserName
        {
            get => _userName;
            set => Set(ref _userName, value);
        }
        #endregion

        #region UserRole
        private string _userRole;
        public string UserRole
        {
            get => _userRole;
            set => Set(ref _userRole, value);
        }
        #endregion

        #endregion

        public WelcomePageViewModel()
        {

        }
    }
}
