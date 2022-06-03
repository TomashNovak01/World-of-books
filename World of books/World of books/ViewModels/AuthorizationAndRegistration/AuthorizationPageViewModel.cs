using System.Windows.Controls;
using System.Windows.Input;
using World_of_books.Infrastructures.Commands;
using World_of_books.ViewModels.Base;

namespace World_of_books.ViewModels.AuthorizationAndRegistration
{
    internal class AuthorizationPageViewModel : ViewModelBase
    {
        #region Fields

        #region RegistrationPage
        private Page _registrationPage;
        public Page RegistrationPage
        {
            get => _registrationPage;
            set => Set(ref _registrationPage, value);
        }
        #endregion

        #endregion

        public AuthorizationPageViewModel()
        {
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

        }
        #endregion

        #endregion
    }
}
