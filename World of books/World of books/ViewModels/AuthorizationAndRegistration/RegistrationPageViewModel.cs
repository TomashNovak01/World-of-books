using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using World_of_books.Data.Classes;
using World_of_books.Infrastructures.Commands;
using World_of_books.Models;
using World_of_books.ViewModels.Base;
using World_of_books.Views.Windows.Customer;

namespace World_of_books.ViewModels.AuthorizationAndRegistration
{
    internal class RegistrationPageViewModel : ViewModelBase
    {
        #region Fields
        #region LastName
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => Set(ref _lastName, value);
        }
        #endregion

        #region FirstName
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => Set(ref _firstName, value);
        }
        #endregion

        #region MiddleName
        private string _middleName;
        public string MiddleName
        {
            get => _middleName;
            set => Set(ref _middleName, value);
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

        #region Email
        private string _email;
        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }
        #endregion

        #region Gender
        private string _gender;
        public string Gender
        {
            get => _gender;
            set => Set(ref _gender, value);
        }
        #endregion

        #region BirthdayDate
        private string _birthdayDate;
        public string BirthdayDate
        {
            get => _birthdayDate;
            set => Set(ref _birthdayDate, value);
        }
        #endregion

        #region NumberPhone
        private string _numberPhone;
        public string NumberPhone
        {
            get => _numberPhone;
            set => Set(ref _numberPhone, value);
        }
        #endregion

        #region ListErrors
        private List<string> _listErrors = new List<string>();
        #endregion
        #endregion

        public RegistrationPageViewModel()
        {
            #region Commands
            OpenAuthorizationPageCommand = new LambdaCommand(_onOpenAuthorizationPageCommandExcuted, _canOpenAuthorizationPageCommandExcute);
            CreateNewAccountCommand = new LambdaCommand(_onCreateNewAccountCommandExcuted, _canCreateNewAccountCommandExcute);
            #endregion
        }

        #region Commands

        #region OpenAuthorizationPageCommand
        public ICommand OpenAuthorizationPageCommand { get; }
        private bool _canOpenAuthorizationPageCommandExcute(object p) => true;
        private void _onOpenAuthorizationPageCommandExcuted(object p) => AuthorAndRegWindowViewModel.MainFrame.GoBack();
        #endregion


        #region CreateNewAccountCommand
        public ICommand CreateNewAccountCommand { get; }
        private bool _canCreateNewAccountCommandExcute(object p) => true;
        private void _onCreateNewAccountCommandExcuted(object p)
        {
            TryLastAndFirstNameAndPassword(_lastName, _firstName, _password);
            TryEmail(_email);
            TryNumberPhone(_numberPhone);

            if (_listErrors.Count > 0)
            {
                MessageBox.Show($"Исправьте поля: {string.Join(", ", _listErrors.Select(e => e))}.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                _listErrors.Clear();
                return;
            }

            SaveData();
            OpenCustomerWindow();
        }

        #region TryData
        private void TryLastAndFirstNameAndPassword(params string[] parameters)
        {
            foreach (var parameter in parameters)
                if (string.IsNullOrEmpty(parameter))
                {
                    _listErrors.Add("фамилия или имя или пароль");
                    return;
                }
        }

        private void TryEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
                var regex = new Regex(pattern, RegexOptions.IgnoreCase);

                if (!regex.IsMatch(email))
                    _listErrors.Add("почта");
            }
            else
                _listErrors.Add("почта");
        }

        private void TryNumberPhone(string phone)
        {
            if (phone.Length < 11 || !long.TryParse(phone, out long number))
                _listErrors.Add("номер телефона");
        }
        #endregion

        private void SaveData()
        {
            int customerRole = 1;

            User newUser = new User()
            {
                IdRole = customerRole,
                Firstname = _firstName,
                Lastname = _lastName,
                Middlename = _middleName,
                Password = _password,
                E_mall = _email,
                Gender = _gender,
                DateOfBirth = Convert.ToDateTime(_birthdayDate),
                NumberPhone = _numberPhone,
            };

            SessionData.CurrentUser = newUser;
            CourseworkEntities.Instance.User.Add(SessionData.CurrentUser);

            OpenCustomerWindow();
        }

        private void OpenCustomerWindow()
        {
            SessionData.CurrentWindow = new CustomerWindow();
            SessionData.CurrentWindow.Show();
        }
        #endregion
        #endregion
    }
}
