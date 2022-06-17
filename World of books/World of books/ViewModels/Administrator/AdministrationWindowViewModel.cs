using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using World_of_books.Data.Classes;
using World_of_books.Infrastructures.Commands;
using World_of_books.ViewModels.Base;
using World_of_books.ViewModels.GeneralViewModels;

namespace World_of_books.ViewModels.Administrator
{
    internal class AdministrationWindowViewModel : ViewModelBase
    {
        #region Fields
        #region Account
        #region LastName
        private string _lastName = SessionData.CurrentUser.Lastname;
        public string LastName
        {
            get => _lastName;
            set => Set(ref _lastName, value);
        }
        #endregion

        #region FirstName
        private string _firstName = SessionData.CurrentUser.Firstname;
        public string FirstName
        {
            get => _firstName;
            set => Set(ref _firstName, value);
        }
        #endregion

        #region Email
        private string _email = SessionData.CurrentUser.E_mall;
        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }
        #endregion

        #region Password
        private string _password = SessionData.CurrentUser.Password;
        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }
        #endregion
        #endregion

        #region All the accounts
        #region Finding
        private string _finding;
        public string Finding
        {
            get => _finding;
            set => Set(ref _finding, value);
        }
        #endregion
        #endregion
        #endregion

        public AdministrationWindowViewModel()
        {
            #region Commands
            SaveChangesCommand = new LambdaCommand(_onSaveChangesCommandExcuted, _canSaveChangesCommandExcute);
            #endregion
        }

        #region Commands
        #region SaveChangesCommand
        public ICommand SaveChangesCommand { get; }
        private bool _canSaveChangesCommandExcute(object p) => true;
        private void _onSaveChangesCommandExcuted(object p)
        {
            if (DataCheck.TryLastAndFirstNameAndPassword(_lastName, _firstName, _password) && DataCheck.TryEmail(_email))
            {
                ChangeData();
                MessageBox.Show("Вы успешно изменили данные о себе.", "Изменение выполнено", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                DataCheck.ShowErrors();
        }

        private void ChangeData()
        {
            SessionData.CurrentUser.Lastname = _lastName;
            SessionData.CurrentUser.Firstname = _firstName;
            SessionData.CurrentUser.Password = _password;
            SessionData.CurrentUser.E_mall = _email;
        }
        #endregion


        #endregion
    }
}
