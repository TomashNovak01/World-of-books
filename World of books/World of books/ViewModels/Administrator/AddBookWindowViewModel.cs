using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using World_of_books.Data.Classes;
using World_of_books.Infrastructures.Commands;
using World_of_books.Models;
using World_of_books.ViewModels.Base;

namespace World_of_books.ViewModels.Administrator
{
    internal class AddBookWindowViewModel : ViewModelBase
    {
        #region Fields
        #region PublishingHouse
        private PublishingHouse _publishingHouse;
        public PublishingHouse PublishingHouse
        {
            get => _publishingHouse;
            set => Set(ref _publishingHouse, value);
        }
        #endregion

        #region DisplayPublishingHouse
        private List<PublishingHouse> _displayPublishingHouse = CourseworkEntities.Instance.PublishingHouse.ToList();
        public List<PublishingHouse> DisplayPublishingHouse
        {
            get => _displayPublishingHouse;
            set => Set(ref _displayPublishingHouse, value);
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

        #region Summary
        private string _summary;
        public string Summary
        {
            get => _summary;
            set => Set(ref _summary, value);
        }
        #endregion

        #region Rating
        private string _rating;
        public string Rating
        {
            get => _rating;
            set => Set(ref _rating, value);
        }
        #endregion

        #region DateOfPublish
        private DateTime _dateOfPublish;
        public DateTime DateOfPublish
        {
            get => _dateOfPublish;
            set => Set(ref _dateOfPublish, value);
        }
        #endregion

        #region NumberOfBooksLeftInStock
        private string _numberOfBooksLeftInStock;
        public string NumberOfBooksLeftInStock
        {
            get => _numberOfBooksLeftInStock;
            set => Set(ref _numberOfBooksLeftInStock, value);
        }
        #endregion

        #region Cost
        private string _cost;
        public string Cost
        {
            get => _cost;
            set => Set(ref _cost, value);
        }
        #endregion
        #endregion

        public AddBookWindowViewModel()
        {
            FillFields();
            SaveDataChangeCommand = new LambdaCommand(_onSaveDataChangeCommandExcuted, _canSaveDataChangeCommandExcute);
        }

        #region Commands
        private void FillFields()
        {
            if (SessionData.SelectedBook != null)
            {
                _publishingHouse = SessionData.SelectedBook.PublishingHouse;
                _title = SessionData.SelectedBook.Title;
                _summary = SessionData.SelectedBook.Summary;
                _rating = SessionData.SelectedBook.Rating.ToString();
                _dateOfPublish = SessionData.SelectedBook.Publication_date;
                _numberOfBooksLeftInStock = SessionData.SelectedBook.NumberOfBooksLeftInStock.ToString();
                _cost = SessionData.SelectedBook.Cost.ToString();
            }
        }

        #region SaveDataChangeCommand
        public ICommand SaveDataChangeCommand { get; }
        private bool _canSaveDataChangeCommandExcute(object p) => true;
        private void _onSaveDataChangeCommandExcuted(object p)
        {
            if (CheckDataOfBook())
            {
                if (SessionData.SelectedBook == null)
                    CourseworkEntities.Instance.Book.Add(new Book()
                    {
                        IdPublishingHouse = _publishingHouse.ID,
                        Title = _title,
                        Summary = _summary,
                        Rating = Convert.ToDouble(_rating),
                        Publication_date = _dateOfPublish,
                        NumberOfBooksLeftInStock = Convert.ToInt32(_numberOfBooksLeftInStock),
                        Cost = Convert.ToDecimal(_cost)
                    });
                else
                {
                    SessionData.SelectedBook.IdPublishingHouse = _publishingHouse.ID;
                    SessionData.SelectedBook.Title = _title;
                    SessionData.SelectedBook.Summary = _summary;
                    SessionData.SelectedBook.Rating = Convert.ToDouble(_rating);
                    SessionData.SelectedBook.Publication_date = _dateOfPublish;
                    SessionData.SelectedBook.NumberOfBooksLeftInStock = Convert.ToInt32(_numberOfBooksLeftInStock);
                    SessionData.SelectedBook.Cost = Convert.ToDecimal(_cost);
                }

                CourseworkEntities.Instance.SaveChanges();
            }
            else
                MessageBox.Show("Проверти на правильность вводимые данные", "Не корректные данные", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private bool CheckDataOfBook()
        {
            List<string> data = new List<string>
            {
                _title,
                _summary,
                _rating,
                _numberOfBooksLeftInStock,
                _cost
            };

            foreach (var item in data)
                if (string.IsNullOrEmpty(item))
                    return false;

            if (!double.TryParse(_rating, out double fResult) ||
                !int.TryParse(_numberOfBooksLeftInStock, out int iResult) ||
                !decimal.TryParse(_cost, out decimal dResult))
                return false;

            if (_publishingHouse == null || _dateOfPublish == null)
                return false;

            return true;
        }
        #endregion
        #endregion
    }
}
