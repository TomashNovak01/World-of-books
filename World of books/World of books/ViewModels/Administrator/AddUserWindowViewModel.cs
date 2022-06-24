using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using World_of_books.Data.Classes;
using World_of_books.Infrastructures.Commands;
using World_of_books.Models;
using World_of_books.ViewModels.Base;
using World_of_books.ViewModels.GeneralViewModels;

namespace World_of_books.ViewModels.Administrator
{
    internal class AddUserWindowViewModel : ViewModelBase
    {
        #region Fields
        #region Role
        private int _role;
        public int Role
        {
            get => _role;
            set => Set(ref _role, value);
        }
        #endregion

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

        #region Title
        private string _title;
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        #endregion

        #region TextOnButton
        private string _textOnButton;
        public string TextOnButton
        {
            get => _textOnButton;
            set => Set(ref _textOnButton, value);
        }
        #endregion

        #region RolesList
        private List<Role> _rolesList = CourseworkEntities.Instance.Role.ToList();
        public List<Role> RolesList
        {
            get => _rolesList;
            set => Set(ref _rolesList, value);
        }
        #endregion
        #endregion

        public AddUserWindowViewModel()
        {
            #region Commands
            EditAndDeleteCheck();
            ChangeUserDataCommand = new LambdaCommand(_onChangeUserDataCommandExcuted, _canChangeUserDataCommandExcute);
            #endregion
        }

        #region Commands
        #region EditAndDeleteCheck
        private void EditAndDeleteCheck()
        {
            if (SessionData.SelectedUser == null)
            {
                _title = "Заполните поля для создание нового пользователя";
                _textOnButton = "Добавить нового пользователя";
            }
            else
            {
                _title = "Заполните поля для изменение пользователя";
                _role = SessionData.SelectedUser.IdRole - 1;
                _firstName = SessionData.SelectedUser.Firstname;
                _lastName = SessionData.SelectedUser.Lastname;
                _middleName = SessionData.SelectedUser.Middlename;
                _password = SessionData.SelectedUser.Password;
                _email = SessionData.SelectedUser.E_mall;
                _gender = SessionData.SelectedUser.Gender;
                _birthdayDate = SessionData.SelectedUser.DateOfBirth.ToString();
                _numberPhone = SessionData.SelectedUser.NumberPhone;
                _textOnButton = "Сохранить изменения";
            }
        }
        #endregion

        #region ChangeUserDataCommand
        public ICommand ChangeUserDataCommand { get; }
        private bool _canChangeUserDataCommandExcute(object p) => true;
        private void _onChangeUserDataCommandExcuted(object p)
        {
            if (DataCheck.TryLastAndFirstNameAndPassword(_lastName, _firstName, _password) &&
                 DataCheck.TryEmail(_email) && DataCheck.TryNumberPhone(_numberPhone))
            {
                if (SessionData.SelectedUser == null)
                    SaveData();
                else
                    ChangeUser();

                SessionData.CurrentDialogue.Close();
            }
                
            else
                DataCheck.ShowErrors();
        }

        private void SaveData()
        {
            User newUser = new User()
            {
                IdRole = _role + 1,
                Firstname = _firstName,
                Lastname = _lastName,
                Middlename = _middleName,
                Password = _password,
                E_mall = _email,
                Gender = _gender,
                DateOfBirth = Convert.ToDateTime(_birthdayDate),
                NumberPhone = _numberPhone,
            };

            CourseworkEntities.Instance.User.Add(newUser);
            CourseworkEntities.Instance.SaveChanges();
        }

        private void ChangeUser()
        {
            SessionData.SelectedUser.IdRole = _role + 1;
            SessionData.SelectedUser.Firstname = _firstName;
            SessionData.SelectedUser.Lastname = _lastName;
            SessionData.SelectedUser.Middlename = _middleName;
            SessionData.SelectedUser.Password = _password;
            SessionData.SelectedUser.E_mall = _email;
            SessionData.SelectedUser.Gender = _gender;
            SessionData.SelectedUser.DateOfBirth = Convert.ToDateTime(_birthdayDate);
            SessionData.SelectedUser.NumberPhone = _numberPhone;

            CourseworkEntities.Instance.SaveChanges();
        }
        #endregion
        #endregion

    }
}
