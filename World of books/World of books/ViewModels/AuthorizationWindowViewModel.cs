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
    internal class AuthorizationWindowViewModel : ViewModelBase
    {
        private string _title = "Test";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        #region Commands

        #region OpenRegistrationWindowCommand
        public ICommand OpenRegistrationWindowCommand { get; }
        private bool _canOpenRegistrationWindowCommandExcute(object p) => true;
        private void _onOpenRegistrationWindowCommandExcuted(object p)
        {
            new RegistrationWindow().Show();
            new AuthorizationWindow().Close();
        }
        #endregion

        #endregion

        public AuthorizationWindowViewModel()
        {
            #region Commands

            OpenRegistrationWindowCommand = new LambdaCommand(_onOpenRegistrationWindowCommandExcuted, _canOpenRegistrationWindowCommandExcute);

            #endregion
        }
    }
}
