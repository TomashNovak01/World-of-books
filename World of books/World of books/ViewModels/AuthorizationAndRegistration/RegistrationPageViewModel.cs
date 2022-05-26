using System.Windows.Input;
using World_of_books.Infrastructures.Commands;
using World_of_books.ViewModels.Base;
using World_of_books.Views.Windows.Authorization;

namespace World_of_books.ViewModels.AuthorizationAndRegistration
{
    internal class RegistrationPageViewModel : ViewModelBase
    {
        #region Fields

        private AuthorizationAndRegistrationWindow _window { get; set; }

        #endregion

        public RegistrationPageViewModel()
        {
            _window = new AuthorizationAndRegistrationWindow();

            #region Commands

            OpenAuthorizationPageCommand = new LambdaCommand(_onOpenAuthorizationPageCommandExcuted, _canOpenAuthorizationPageCommandExcute);

            #endregion
        }

        #region Commands

        #region OpenAuthorizationPageCommand
        public ICommand OpenAuthorizationPageCommand { get; }
        private bool _canOpenAuthorizationPageCommandExcute(object p) => true;
        private void _onOpenAuthorizationPageCommandExcuted(object p)
        {
            _window.mainFrame.Content = null;
            _window.mainFrame.Navigate(new AuthorizationPage());
        }
        #endregion

        #endregion
    }
}
