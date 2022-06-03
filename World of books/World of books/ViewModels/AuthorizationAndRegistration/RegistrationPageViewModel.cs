using System.Windows.Controls;
using System.Windows.Input;
using World_of_books.Infrastructures.Commands;
using World_of_books.ViewModels.Base;
using World_of_books.Views.Windows.Authorization;

namespace World_of_books.ViewModels.AuthorizationAndRegistration
{
    internal class RegistrationPageViewModel : ViewModelBase
    {
        #region Fields


        #region AuthorizationPage
        private Page _authorizationPage = new AuthorizationPage();
        public Page AuthorizationPage
        {
            get => _authorizationPage;
            set => Set(ref _authorizationPage, value);
        }
        #endregion
        #endregion

        public RegistrationPageViewModel()
        {
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

        }
        #endregion

        #endregion
    }
}
