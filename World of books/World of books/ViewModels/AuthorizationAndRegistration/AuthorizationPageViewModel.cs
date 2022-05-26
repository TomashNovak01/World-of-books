using System.Windows.Input;
using World_of_books.Infrastructures.Commands;
using World_of_books.ViewModels.Base;
using World_of_books.Views.Windows.Authorization;

namespace World_of_books.ViewModels.AuthorizationAndRegistration
{
    internal class AuthorizationPageViewModel : ViewModelBase
    {
        #region Fields

        private AuthorizationAndRegistrationWindow _window { get; set; }

        #endregion

        public AuthorizationPageViewModel()
        {
            _window = new AuthorizationAndRegistrationWindow();

            #region Commands

            OpenRegistrationPageCommand = new LambdaCommand(_onOpenRegistrationPageCommandExcuted, _canOpenRegistrationPageCommandExcute);

            #endregion
        }

        #region Commands

        #region OpenRegistrationPageCommand
        public ICommand OpenRegistrationPageCommand { get; }
        private bool _canOpenRegistrationPageCommandExcute(object p) => true;
        private void _onOpenRegistrationPageCommandExcuted(object p)
        {
            _window.mainFrame.Content = null;
            _window.mainFrame.Navigate(new RegistrationPage());
        }
        #endregion

        #endregion
    }
}
