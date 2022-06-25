using System;
using System.Windows;
using System.Windows.Input;
using World_of_books.Data.Classes;
using World_of_books.Infrastructures.Commands;
using World_of_books.Models;
using World_of_books.ViewModels.Base;

namespace World_of_books.ViewModels.Administrator
{
    internal class AddAuthorWindowViewModel : ViewModelBase
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

        #region BirthdayDate
        private string _birthdayDate;
        public string BirthdayDate
        {
            get => _birthdayDate;
            set => Set(ref _birthdayDate, value);
        }
        #endregion

        #region DateOfDeath
        private string _dateOfDeath;
        public string DateOfDeath
        {
            get => _dateOfDeath;
            set => Set(ref _dateOfDeath, value);
        }
        #endregion

        #region ShortBiography
        private string _shortBiography;
        public string ShortBiography
        {
            get => _shortBiography;
            set => Set(ref _shortBiography, value);
        }
        #endregion
        #endregion

        public AddAuthorWindowViewModel()
        {
            AddAuthorCommand = new LambdaCommand(_onAddAuthorCommandExcuted, _canAddAuthorCommandExcute);
        }

        #region Commands
        #region AddAuthorCommand
        public ICommand AddAuthorCommand { get; }
        private bool _canAddAuthorCommandExcute(object p) => true;
        private void _onAddAuthorCommandExcuted(object p)
        {
            if (!string.IsNullOrEmpty(_firstName))
            {
                CourseworkEntities.Instance.Author.Add(new Author()
                {
                    Lastname = _lastName,
                    Firstname = _firstName,
                    Middlename = _middleName,
                    DateOfBirth = Convert.ToDateTime(_birthdayDate),
                    DateOfDeath = Convert.ToDateTime(_dateOfDeath),
                    ShortBiography = _shortBiography,
                });
                CourseworkEntities.Instance.SaveChanges();
                SessionData.CurrentDialogue.Close();
            }
            else
                MessageBox.Show("Заполните хотя бы имя", "Поле пустое", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        #endregion
        #endregion
    }
}
