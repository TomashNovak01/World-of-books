using World_of_books.Models;
using World_of_books.ViewModels.Base;

namespace World_of_books.ViewModels.UserControls
{
    internal class BookControlViewModel : ViewModelBase
    {
        #region Fields
        private static Book _currentBook;

        #region Title
        private string _title = _currentBook.Title;
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        #endregion

        #region Summary
        private string _summary = _currentBook.Summary;
        public string Summary
        {
            get => _summary;
            set => Set(ref _summary, value);
        }
        #endregion

        #region Cost
        private decimal _cost = _currentBook.Cost;
        public decimal Cost
        {
            get => _cost;
            set => Set(ref _cost, value);
        }
        #endregion

        #region Rating
        private double _rating = _currentBook.Rating;
        public double Rating
        {
            get => _rating;
            set => Set(ref _rating, value);
        }
        #endregion

        #region PublishHouse
        private string _publishHouse = _currentBook.PublishingHouse.Title;
        public string PublishHouse
        {
            get => _publishHouse;
            set => Set(ref _publishHouse, value);
        }
        #endregion
        #endregion

        public BookControlViewModel(Book currentBook)
        {
            _currentBook = currentBook;
        }
    }
}
