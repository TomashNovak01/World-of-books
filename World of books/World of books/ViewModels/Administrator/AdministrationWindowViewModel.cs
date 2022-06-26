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

        #region SelectedUser
        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set => Set(ref _selectedUser, value);
        }
        #endregion

        #region RolesList
        private List<Role> _rolesList = new List<Role>();
        public List<Role> RolesList
        {
            get => _rolesList;
            set => Set(ref _rolesList, value);
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

        #region SelectedBook
        private Book _selectedBook;
        public Book SelectedBook
        {
            get => _selectedBook;
            set => Set(ref _selectedBook, value);
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
                UpdateBooksList();
            }
        }
        #endregion

        #region Authors
        #region IdAuthor
        private int _idAuthor;
        public int IdAuthor
        {
            get => _idAuthor;
            set
            {
                Set(ref _idAuthor, value);
                UpdateBooksList();
            }
        }
        #endregion

        #region AuthorsList
        private List<Author> _authorsList = new List<Author>();
        public List<Author> AuthorsList
        {
            get => _authorsList;
            set => Set(ref _authorsList, value);
        }
        #endregion
        #endregion

        #region Genre
        #region IdGenre
        private int _idGenre;
        public int IdGenre
        {
            get => _idGenre;
            set
            {
                Set(ref _idGenre, value);
                UpdateBooksList();
            }
        }
        #endregion

        #region SelectedGenre
        private Genre _selectedGenre;
        public Genre SelectedGenre
        {
            get => _selectedGenre;
            set => Set(ref _selectedGenre, value);
        }
        #endregion

        #region GenresList
        private List<Genre> _genresList = new List<Genre>();
        public List<Genre> GenresList
        {
            get => _genresList;
            set => Set(ref _genresList, value);
        }
        #endregion
        #endregion

        #region Tags
        #region IdTag
        private int _idTag;
        public int IdTag
        {
            get => _idTag;
            set
            {
                Set(ref _idTag, value);
                UpdateBooksList();
            }
        }
        #endregion

        #region TagsList
        private List<Tag> _tagsList = new List<Tag>();
        public List<Tag> TagsList
        {
            get => _tagsList;
            set => Set(ref _tagsList, value);
        }
        #endregion
        #endregion
        #endregion
        #endregion

        public AdministrationWindowViewModel()
        {
            #region Commands
            #region Account
            SaveChangesCommand = new LambdaCommand(_onSaveChangesCommandExcuted, _canSaveChangesCommandExcute);
            #endregion

            #region All the accounts
            FillRolesList();

            OpenAddUserWindowCommand = new LambdaCommand(_onOpenAddUserWindowCommandExcuted, _canOpenAddUserWindowCommandExcute);
            ChangeUserCommand = new LambdaCommand(_onChangeUserCommandExcuted, _canChangeUserCommandExcute);
            DeleteUserCommand = new LambdaCommand(_onDeleteUserCommandExcuted, _canDeleteUserCommandExcute);
            #endregion

            #region All the books
            FillAuthorList();
            FillGenresList();
            FillTagsList();

            OpenAddBookWindowCommand = new LambdaCommand(_onOpenAddBookWindowCommandExcuted, _canOpenAddBookWindowCommandExcute);
            ChangeBookCommand = new LambdaCommand(_onChangeBookCommandExcuted, _canChangeBookCommandExcute);
            DeleteBookCommand = new LambdaCommand(_onDeleteBookCommandExcuted, _canDeleteBookCommandExcute);

            OpenAddGenreOrTagWindowCommand = new LambdaCommand(_onOpenAddGenreOrTagWindowCommandExcuted, _canOpenAddGenreOrTagWindowCommandExcute);
            OpenAddAuthorWindowCommand = new LambdaCommand(_onOpenAddAuthorWindowCommandExcuted, _canOpenAddAuthorWindowCommandExcute);
            #endregion
            #endregion
        }

        #region Commands
        #region Account
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
        #endregion

        #region All the accounts
        private void FillRolesList()
        {
            _rolesList.Add(new Role() { Name = "Все роли" });
            foreach (var role in CourseworkEntities.Instance.Role)
                _rolesList.Add(role);
        }

        #region OpenAddUserWindowCommand
        public ICommand OpenAddUserWindowCommand { get; }
        private bool _canOpenAddUserWindowCommandExcute(object p) => true;
        private void _onOpenAddUserWindowCommandExcuted(object p)
        {
            SessionData.SelectedUser = null;

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
                MessageBox.Show("По вашему запросу пользователи не найдены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        #endregion

        #region ChangeUserCommand
        public ICommand ChangeUserCommand { get; }
        private bool _canChangeUserCommandExcute(object p) => true;
        private void _onChangeUserCommandExcuted(object p)
        {
            if (SelectedUser != null)
            {
                SessionData.SelectedUser = _selectedUser;

                SessionData.CurrentDialogue = new AddUserWindow();
                SessionData.CurrentDialogue.ShowDialog();

                UpdateUsersList();
            }
            else
                MessageBox.Show("Перед изменением, выберите пользователя", "Пользователь не выбран", MessageBoxButton.OK, MessageBoxImage.Warning);
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

        #region All the books
        #region FillLists
        private void FillGenresList()
        {
            _genresList.Clear();

            _genresList.Add(new Genre() { Title = "Все жанры" });
            foreach (var genre in CourseworkEntities.Instance.Genre)
                _genresList.Add(genre);
        }

        private void FillTagsList()
        {
            _tagsList.Clear();

            _tagsList.Add(new Tag() { Title = "Все тэги" });
            foreach (var tag in CourseworkEntities.Instance.Tag)
                _tagsList.Add(tag);
        }

        private void FillAuthorList()
        {
            _authorsList.Clear();

            _authorsList.Add(new Author() { Lastname = "Все авторы" });
            foreach (var author in CourseworkEntities.Instance.Author)
                _authorsList.Add(author);
        }
        #endregion

        private void UpdateBooksList()
        {
            DisplayBooks = CourseworkEntities.Instance.Book.ToList();

            if (!string.IsNullOrEmpty(_findingBook) && _findingBook.ToLower() is var search)
                DisplayBooks = DisplayBooks.Where(b => b.Title.ToLower().Contains(search) ||
                                                    b.Summary.ToLower().Contains(search)).ToList();

            if (_idAuthor != 0)
                DisplayBooks = DisplayBooks.Where(b => b.Author.Any(a => a.ID == _idAuthor)).ToList();

            if (_idGenre != 0)
                DisplayBooks = DisplayBooks.Where(b => b.Genre.Any(a => a.ID == _idGenre)).ToList();

            if (_idTag != 0)
                DisplayBooks = DisplayBooks.Where(b => b.Tag.Any(t => t.Id == _idTag)).ToList();

            if (DisplayUsers.Count == 0)
                MessageBox.Show("По вашему запросу книги не найдены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        #region OpenAddBookWindowCommand
        public ICommand OpenAddBookWindowCommand { get; }
        private bool _canOpenAddBookWindowCommandExcute(object p) => true;
        private void _onOpenAddBookWindowCommandExcuted(object p)
        {
            SessionData.SelectedBook = null;

            SessionData.CurrentDialogue = new AddBookWindow();
            SessionData.CurrentDialogue.ShowDialog();

            UpdateBooksList();
        }
        #endregion

        #region OpenAddGenreOrTagWindowCommand
        public ICommand OpenAddGenreOrTagWindowCommand { get; }
        private bool _canOpenAddGenreOrTagWindowCommandExcute(object p) => true;
        private void _onOpenAddGenreOrTagWindowCommandExcuted(object p)
        {
            SessionData.CurrentDialogue = new AddGenreOrTagWindow();
            SessionData.CurrentDialogue.ShowDialog();

            FillGenresList();
            FillTagsList();
        }
        #endregion

        #region OpenAddAuthorWindowCommand
        public ICommand OpenAddAuthorWindowCommand { get; }
        private bool _canOpenAddAuthorWindowCommandExcute(object p) => true;
        private void _onOpenAddAuthorWindowCommandExcuted(object p)
        {
            SessionData.CurrentDialogue = new AddAuthorWindow();
            SessionData.CurrentDialogue.ShowDialog();

            FillAuthorList();
        }
        #endregion

        #region ChangeBookCommand
        public ICommand ChangeBookCommand { get; }
        private bool _canChangeBookCommandExcute(object p) => true;
        private void _onChangeBookCommandExcuted(object p)
        {
            if (SelectedBook != null)
            {
                SessionData.SelectedBook = _selectedBook;

                SessionData.CurrentDialogue = new AddBookWindow();
                SessionData.CurrentDialogue.ShowDialog();

                UpdateBooksList();
            }
            else
                MessageBox.Show("Перед изменением, выберите книгу", "Книга не выбран", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        #endregion

        #region DeleteBookCommand
        public ICommand DeleteBookCommand { get; }
        private bool _canDeleteBookCommandExcute(object p) => true;
        private void _onDeleteBookCommandExcuted(object p)
        {
            if (SelectedBook != null)
            {
                CourseworkEntities.Instance.Book.Remove(SelectedBook);
                CourseworkEntities.Instance.SaveChanges();

                UpdateBooksList();
            }
            else
                MessageBox.Show("Перед удалением, выберите книгу", "Пользователь не выбран", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        #endregion
        #endregion
        #endregion
    }
}
