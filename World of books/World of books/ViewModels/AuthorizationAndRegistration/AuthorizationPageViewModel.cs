﻿using System.Windows.Controls;
using System.Windows.Input;
using World_of_books.Infrastructures.Commands;
using World_of_books.ViewModels.Base;
using World_of_books.Views.Windows.Authorization;

namespace World_of_books.ViewModels.AuthorizationAndRegistration
{
    internal class AuthorizationPageViewModel : ViewModelBase
    {
        #region Fields

        #region RegistrationPage
        private Page _registrationPage = new RegistrationPage();
        public Page RegistrationPage
        {
            get => _registrationPage;
            set => Set(ref _registrationPage, value);
        }
        #endregion

        #region Login
        private string _login;
        public string Login
        {
            get => _login;
            set => Set(ref _login, value);
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
        #endregion

        public AuthorizationPageViewModel()
        {
            #region Commands

            LogInCommand = new LambdaCommand(_onLogInCommandExcuted, _canLogInCommandExcute);
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

        #region LogInCommand
        public ICommand LogInCommand { get; }
        private bool _canLogInCommandExcute(object p)
        {
            return true;
        }
        private void _onLogInCommandExcuted(object p)
        {
            
        }
        #endregion

        #endregion
    }
}