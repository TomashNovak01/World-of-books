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
        #region Fields
        #region AccountData
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

        #region MiddleName
        private string _middleName = SessionData.CurrentUser.Middlename;
        public string MiddleName
        {
            get => _middleName;
            set => Set(ref _middleName, value);
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

        #region Email
        private string _email = SessionData.CurrentUser.E_mall;
        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }
        #endregion

        #region Gender
        private string _gender = SessionData.CurrentUser.Gender;
        public string Gender
        {
            get => _gender;
            set => Set(ref _gender, value);
        }
        #endregion

        #region BirthdayDate
        private string _birthdayDate = SessionData.CurrentUser.DateOfBirth.ToString();
        public string BirthdayDate
        {
            get => _birthdayDate;
            set => Set(ref _birthdayDate, value);
        }
        #endregion

        #region NumberPhone
        private string _numberPhone = SessionData.CurrentUser.NumberPhone;
        public string NumberPhone
        {
            get => _numberPhone;
            set => Set(ref _numberPhone, value);
        }
        #endregion
        #endregion
        #endregion

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
