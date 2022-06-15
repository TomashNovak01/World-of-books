using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using World_of_books.Data.Classes;
using World_of_books.Infrastructures.Commands;
using World_of_books.ViewModels.Base;
using World_of_books.Views.Windows.Authorization;

namespace World_of_books.ViewModels.Customer
{
    internal class CustomerWindowViewModel : ViewModelBase
    {
        public CustomerWindowViewModel()
        {
            #region Commands
            OpenStartWindowCommand = new LambdaCommand(_onOpenStartWindowCommandExcuted, _canOpenStartWindowCommandExcute);
            #endregion
        }

        #region Commands
        #region OpenStartWindowCommand
        public ICommand OpenStartWindowCommand { get; }
        private bool _canOpenStartWindowCommandExcute(object p) => true;
        private void _onOpenStartWindowCommandExcuted(object p)
        {
            SessionData.CurrentWindow = new AuthorizationAndRegistrationWindow();
            SessionData.CurrentWindow.Show();
        }
        #endregion
        #endregion
    }
}
