using System.Windows.Input;
using World_of_books.Infrastructures.Commands;
using World_of_books.ViewModels.Base;
using World_of_books.Views.Windows.Authorization;

namespace World_of_books.ViewModels.AuthorizationAndRegistration
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


            #endregion
        }

        #region Commands

       

        #endregion
    }
}
