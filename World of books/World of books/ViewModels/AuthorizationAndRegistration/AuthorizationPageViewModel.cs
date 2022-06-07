using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using World_of_books.Data.Classes;
using World_of_books.Infrastructures.Commands;
using World_of_books.Models;
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

        #region Email
        private string _email;
        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
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
            //AuthorAndRegWindowViewModel viewModel = new AuthorAndRegWindowViewModel();
            //viewModel.DefaultPage = RegistrationPage;

            // Переход на страницу "Регистрация".
        }
        #endregion

        #region LogInCommand
        public ICommand LogInCommand { get; }
        private bool _canLogInCommandExcute(object p) => true;
        private void _onLogInCommandExcuted(object p)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                MessageBox.Show("Введите недостающие данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                SessionData.CurrentUser = CourseworkEntities.Instance.User.Where(u => u.E_mall == Email && u.Password == Password).FirstOrDefault();

            if (SessionData.CurrentUser == null)
                MessageBox.Show("Пользователь не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            // Переход на страницу "Добро пожаловать".
        }
        #endregion

        #endregion
    }
}
