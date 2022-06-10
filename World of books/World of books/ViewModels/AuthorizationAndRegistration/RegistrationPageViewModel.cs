using System.Windows.Input;
using World_of_books.Infrastructures.Commands;
using World_of_books.ViewModels.Base;

namespace World_of_books.ViewModels.AuthorizationAndRegistration
{
    internal class RegistrationPageViewModel : ViewModelBase
    {
        #region Fields
        #region LastName
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => Set(ref _lastName, value);
        }
        #endregion

        #region FirstName
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => Set(ref _firstName, value);
        }
        #endregion

        #region MiddleName
        private string _middleName;
        public string MiddleName
        {
            get => _middleName;
            set => Set(ref _middleName, value);
        }
        #endregion

        #region Password
        private string _password;
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }
        #endregion

        #region Email
        private string _email;
        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }
        #endregion

        #region NumberPhone
        private string _numberPhone;
        public string NumberPhone
        {
            get => _numberPhone;
            set => Set(ref _numberPhone, value);
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
        private void _onOpenAuthorizationPageCommandExcuted(object p) => AuthorAndRegWindowViewModel.MainFrame.GoBack();
        #endregion

        #endregion
    }
}
