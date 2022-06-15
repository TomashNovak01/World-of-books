using System.Windows.Input;
using World_of_books.Data.Classes;
using World_of_books.Infrastructures.Commands;
using World_of_books.ViewModels.Base;
using World_of_books.Views.Windows.Administration;
using World_of_books.Views.Windows.Customer;

namespace World_of_books.ViewModels.AuthorizationAndRegistration
{
    internal class WelcomePageViewModel : ViewModelBase
    {
        #region Fields
        #region Roles
        const int CUSTOMER_ROLE = 1;
        const int ADMINISTRATION_ROLE = 2;
        #endregion

        #region UserName
        private string _userName = $"{SessionData.CurrentUser.Lastname} {SessionData.CurrentUser.Firstname} {SessionData.CurrentUser.Middlename}";
        public string UserName
        {
            get => _userName;
            set => Set(ref _userName, value);
        }
        #endregion

        #region UserRole
        private string _userRole = SessionData.CurrentUser.Role.Name;
        public string UserRole
        {
            get => _userRole;
            set => Set(ref _userRole, value);
        }
        #endregion

        #endregion

        public WelcomePageViewModel()
        {
            #region Commands
            OpenNewWindowCommand = new LambdaCommand(_onOpenNewWindowCommandExcuted, _canOpenNewWindowCommandExcute);
            #endregion
        }

        #region Commands
        #region OpenNewWindowCommand
        public ICommand OpenNewWindowCommand { get; }
        private bool _canOpenNewWindowCommandExcute(object p) => true;
        private void _onOpenNewWindowCommandExcuted(object p)
        {
            if (SessionData.CurrentUser.IdRole == ADMINISTRATION_ROLE)
                SessionData.CurrentWindow = new AdministrationWindow();
            else if (SessionData.CurrentUser.IdRole == CUSTOMER_ROLE)
                SessionData.CurrentWindow = new CustomerWindow();

            SessionData.CurrentWindow.Show();
        }
        #endregion
        #endregion
    }
}
