using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using World_of_books.Data.Classes;
using World_of_books.Infrastructures.Commands;
using World_of_books.Models;
using World_of_books.ViewModels.Base;
using World_of_books.ViewModels.GeneralViewModels;
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

        #region Basket
        private XWPFDocument _document { get; set; }
        private List<(XWPFParagraph paragraph, CT_Bookmark bookmark)> _marks = new List<(XWPFParagraph paragraph, CT_Bookmark bookmark)>(1);

        #region ShopingCart
        private List<Book> _shopingCart = new List<Book>();
        public List<Book> ShopingCart
        {
            get => _shopingCart;
            set => Set(ref _shopingCart, value);
        }
        #endregion

        #region SelectedInBook
        private Book _selectedInBook;
        public Book SelectedInBook
        {
            get => _selectedInBook;
            set => Set(ref _selectedInBook, value);
        }
        #endregion

        private Order _newOrder { get; set; }
        #endregion
        #endregion

        public CustomerWindowViewModel()
        {
            #region Commands
            OpenStartWindowCommand = new LambdaCommand(_onOpenStartWindowCommandExcuted, _canOpenStartWindowCommandExcute);
            ChangeDataCommand = new LambdaCommand(_onChangeDataCommandExcuted, _canChangeDataCommandExcute);

            FillAuthorList();
            FillGenresList();
            FillTagsList();

            AddBookInBasketCommand = new LambdaCommand(_onAddBookInBasketCommandExcuted, _canAddBookInBasketCommandExcute);
            DeleteBookFromBasketCommand = new LambdaCommand(_onDeleteBookFromBasketCommandExcuted, _canDeleteBookFromBasketCommandExcute);
            FormOrderCommand = new LambdaCommand(_onFormOrderCommandExcuted, _canFormOrderCommandExcute);
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

        #region ChangeDataCommand
        public ICommand ChangeDataCommand { get; }
        private bool _canChangeDataCommandExcute(object p) => true;
        private void _onChangeDataCommandExcuted(object p)
        {
            if (DataCheck.TryLastAndFirstNameAndPassword(_lastName, _firstName, _password) &&
                DataCheck.TryEmail(_email) && DataCheck.TryNumberPhone(_numberPhone))
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
            SessionData.CurrentUser.Middlename = _middleName;
            SessionData.CurrentUser.Password = _password;
            SessionData.CurrentUser.Gender = _gender;
            SessionData.CurrentUser.E_mall = _email;
            SessionData.CurrentUser.DateOfBirth = Convert.ToDateTime(_birthdayDate);
            SessionData.CurrentUser.NumberPhone = _numberPhone;

            CourseworkEntities.Instance.SaveChanges();
        }
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

            if (DisplayBooks.Count == 0)
                MessageBox.Show("По вашему запросу книги не найдены", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        #region AddBookInBasketCommand
        public ICommand AddBookInBasketCommand { get; }
        private bool _canAddBookInBasketCommandExcute(object p) => true;
        private void _onAddBookInBasketCommandExcuted(object p)
        {
            if (_selectedBook != null)
            {
                if (!ShopingCart.Contains(_selectedBook))
                {
                    ShopingCart.Add(_selectedBook);
                }
                else
                    MessageBox.Show("Эта книга уже находится в корзине", "Оповещение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Сначала выберите книгу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion
        #endregion

        #region Basket
        #region DeleteBookFromBasketCommand
        public ICommand DeleteBookFromBasketCommand { get; }
        private bool _canDeleteBookFromBasketCommandExcute(object p) => true;
        private void _onDeleteBookFromBasketCommandExcuted(object p)
        {
            if (_selectedInBook != null)
                ShopingCart.Remove(_selectedInBook);
            else
                MessageBox.Show("Сначала выберите книгу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion

        #region FormOrderCommand
        public ICommand FormOrderCommand { get; }
        private bool _canFormOrderCommandExcute(object p) => true;
        private void _onFormOrderCommandExcuted(object p)
        {
            CreateNewOrder();   
            InitializeDocument();

            MessageBox.Show("Спасибо за покупку", "Благодарность", MessageBoxButton.OK);
            ShopingCart.Clear();
        }

        #region CreateNewOrder
        private void CreateNewOrder()
        {
            _newOrder = new Order()
            {
                IdUser = SessionData.CurrentUser.ID,
                DateOfOrder = DateTime.Now,
                OrderPrice = _shopingCart.Sum(b => b.Cost)
            };
            CourseworkEntities.Instance.Order.Add(_newOrder);

            foreach (var book in _shopingCart)
            {
                CourseworkEntities.Instance.Basket.Add(new Basket()
                {
                    IdOrder = _newOrder.ID,
                    IdBook = book.ID,
                    IdTypeBook = 1,
                    Count = 1
                });
            }

            CourseworkEntities.Instance.SaveChanges();
        }
        #endregion

        #region FormWordFile
        public void InitializeDocument()
        {
            using (Stream stream = new FileStream(Directory.GetCurrentDirectory() + "\\TeplateOfCheck.docx", FileMode.Open))
                _document = new XWPFDocument(stream);

            GetMarks();
            FormatContentInMarks();

            SaveDocument();
        }

        private void GetMarks()
        {
            foreach (XWPFTable table in _document.Tables)
                foreach (XWPFTableRow row in table.Rows)
                    foreach (XWPFTableCell cell in row.GetTableCells())
                        foreach (XWPFParagraph paragraph in cell.Paragraphs)
                        {
                            var ct = paragraph.GetCTP();

                            foreach (CT_Bookmark bookmark in ct.GetBookmarkStartList())
                                _marks.Add((paragraph, bookmark));
                        }
        }

        private void FormatContentInMarks()
        {
            foreach (var mark in _marks)
            {
                switch (mark.bookmark.name)
                {
                    case "IdOrder":
                        mark.paragraph.ReplaceText("{Id Order}", _newOrder.ID.ToString());
                        break;

                    case "DateOfOrder":
                        mark.paragraph.ReplaceText("{Date of Order}", _newOrder.DateOfOrder.ToString());
                        break;

                    case "Customer":
                        mark.paragraph.ReplaceText("{Customer}", _newOrder.User.FullName);
                        break;
                }

                foreach (var book in _shopingCart)
                    switch (mark.bookmark.name)
                    {
                        case "IdBook":
                            mark.paragraph.CreateRun().SetText(book.ID.ToString());
                            break;

                        case "TitleBook":
                            mark.paragraph.CreateRun().SetText(book.Title);
                            break;

                        case "PublishingHouse":
                            mark.paragraph.CreateRun().SetText(book.PublishingHouse.Title);
                            break;

                        case "Cost":
                            mark.paragraph.CreateRun().SetText(book.Cost.ToString());
                            break;

                        case "Count":
                            mark.paragraph.CreateRun().SetText("1");
                            break;

                        case "TypeOfBook":
                            mark.paragraph.CreateRun().SetText("Электронная");
                            break;
                    }
            }
        }

        private void SaveDocument()
        {
            using (Stream stream = new FileStream(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Check.docx"), FileMode.Create))
                _document.Write(stream);
        }
        #endregion
        #endregion
        #endregion
        #endregion
    }
}
