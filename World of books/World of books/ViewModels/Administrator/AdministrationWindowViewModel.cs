using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using World_of_books.Data.Classes;
using World_of_books.Infrastructures.Commands;
using World_of_books.Models;
using World_of_books.ViewModels.Base;
using World_of_books.ViewModels.GeneralViewModels;
using World_of_books.Views.Windows.Administration;

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
        #region DisplayUsers
        private List<User> _displayUsers = CourseworkEntities.Instance.User.ToList();
        public List<User> DisplayUsers
        {
            get => _displayUsers;
            set => Set(ref _displayUsers, value);
        }
        #endregion

        #region FindingUser
        private string _findingUser;
        public string FindingUser
        {
            get => _findingUser;
            set
            {
                Set(ref _findingUser, value);
                UpdateUsersList();
            }
        }
        #endregion

        #region Role
        private int _role;
        public int Role
        {
            get => _role;
            set
            {
                Set(ref _role, value);
                UpdateUsersList();
            }
        }
        #endregion
        #endregion

        #region All the books
        #region DisplayBooks
        private List<Book> _displayBooks = CourseworkEntities.Instance.Book.ToList();
        public List<Book> DisplayBooks
        {
            get => _displayBooks;
            set => Set(ref _displayBooks, value);
        }
        #endregion

        #region SelectedUser
        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set => Set(ref _selectedUser, value);
        }
        #endregion

        #region FindingBook
        private string _findingBook;
        public string FindingBook
        {
            get => _findingBook;
            set
            {
                Set(ref _findingBook, value);
            }
        }
        #endregion
        #endregion
        #endregion

        public AdministrationWindowViewModel()
        {
            #region Commands
            SaveChangesCommand = new LambdaCommand(_onSaveChangesCommandExcuted, _canSaveChangesCommandExcute);
            OpenAddUserWindowCommand = new LambdaCommand(_onOpenAddUserWindowCommandExcuted, _canOpenAddUserWindowCommandExcute);

            ChangeUserCommand = new LambdaCommand(_onChangeUserCommandExcuted, _canChangeUserCommandExcute);
            DeleteUserCommand = new LambdaCommand(_onDeleteUserCommandExcuted, _canDeleteUserCommandExcute);
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

            CourseworkEntities.Instance.SaveChanges();
        }
        #endregion

        #region OpenAddUserWindowCommand
        public ICommand OpenAddUserWindowCommand { get; }
        private bool _canOpenAddUserWindowCommandExcute(object p) => true;
        private void _onOpenAddUserWindowCommandExcuted(object p)
        {
            SessionData.CurrentDialogue = new AddUserWindow();
            SessionData.CurrentDialogue.ShowDialog();

            UpdateUsersList();
        }
        #endregion

        #region UpdateUsersList
        private void UpdateUsersList()
        {
            DisplayUsers = CourseworkEntities.Instance.User.ToList();

            if (!string.IsNullOrEmpty(_findingUser) && _findingUser.ToLower() is var search)
                DisplayUsers = DisplayUsers.Where(u => u.Lastname.ToLower().Contains(search) ||
                                                    u.Firstname.ToLower().Contains(search) ||
                                                    u.Middlename != null &&
                                                    u.Middlename.ToLower().Contains(search)).ToList();

            if (_role != 0)
                DisplayUsers = DisplayUsers.Where(u => u.IdRole == _role).ToList();

            if (DisplayUsers.Count == 0)
                MessageBox.Show("По вашему запросу товары не найдены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        #endregion

        #region ChangeUserCommand
        public ICommand ChangeUserCommand { get; }
        private bool _canChangeUserCommandExcute(object p) => true;
        private void _onChangeUserCommandExcuted(object p)
        {
            MessageBox.Show("Change");
        }
        #endregion

        #region DeleteUserCommand
        public ICommand DeleteUserCommand { get; }
        private bool _canDeleteUserCommandExcute(object p) => true;
        private void _onDeleteUserCommandExcuted(object p)
        {
            if (SelectedUser != null)
            {
                CourseworkEntities.Instance.User.Remove(SelectedUser);
                CourseworkEntities.Instance.SaveChanges();

                UpdateUsersList();
            }
            else
                MessageBox.Show("Перед удалением, выберите пользователя", "Пользователь не выбран", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        #endregion
        #endregion
    }
}
