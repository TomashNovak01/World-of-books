using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using World_of_books.Infrastructures.Commands;
using World_of_books.ViewModels.Base;

namespace World_of_books.ViewModels
{
    internal class AuthorAndRegWindowViewModel : ViewModelBase
    {
        #region Fields

        #region Title
        private string _title = "Test";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        #endregion

        #endregion

        public AuthorAndRegWindowViewModel()
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
