using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using World_of_books.Infrastructures.Commands;
using World_of_books.ViewModels.Base;
using World_of_books.Views.Windows.Authorization;

namespace World_of_books.ViewModels
{
    internal class AuthorAndRegWindowViewModel : ViewModelBase
    {
        #region Fields

        private AuthorizationAndRegistrationWindow _window { get; set; }

        #region Title
        private string _title = "Авторизация";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        #endregion

        #endregion

        public AuthorAndRegWindowViewModel()
        {
            _window = new AuthorizationAndRegistrationWindow();

            #region Commands

            OpenAuthorizationPageCommand = new LambdaCommand(_onOpenAuthorizationPageCommandExcuted, _canOpenAuthorizationPageCommandExcute);
            OpenRegistrationPageCommand = new LambdaCommand(_onOpenRegistrationPageCommandExcuted, _canOpenRegistrationPageCommandExcute);

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
