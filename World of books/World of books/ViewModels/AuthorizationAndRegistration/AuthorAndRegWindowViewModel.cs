using System.Windows.Controls;
using World_of_books.ViewModels.Base;
using World_of_books.Views.Windows.Authorization;

namespace World_of_books.ViewModels.AuthorizationAndRegistration
{
    internal class AuthorAndRegWindowViewModel : ViewModelBase
    {
        #region Fields
        #region Title
        private string _title = "World of books";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        #endregion

        #region DefaultPage
        private Page _defaultPage = new AuthorizationPage();
        public Page DefaultPage
        {
            get => _defaultPage;
            set => Set(ref _defaultPage, value);
        }
        #endregion

        #region MainFrame
        public static Frame MainFrame { get; set; }
        #endregion
        #endregion
    }
}
