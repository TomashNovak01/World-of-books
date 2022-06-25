using System.Windows;
using System.Windows.Input;
using World_of_books.Infrastructures.Commands;
using World_of_books.Models;
using World_of_books.ViewModels.Base;

namespace World_of_books.ViewModels.Administrator
{
    internal class AddGenreOrTagViewModel : ViewModelBase
    {
        #region Fields
        #region Header
        private string _header;
        public string Header
        {
            get => _header;
            set => Set(ref _header, value);
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

        #region GenreOrTag
        private int _genreOrTag;
        public int GenreOrTag
        {
            get => _genreOrTag;
            set => Set(ref _genreOrTag, value);
        }
        #endregion
        #endregion

        public AddGenreOrTagViewModel()
        {
            AddItemCommand = new LambdaCommand(_onAddItemCommandExcuted, _canAddItemCommandExcute);
        }

        #region Commands
        #region AddItemCommand
        public ICommand AddItemCommand { get; }
        private bool _canAddItemCommandExcute(object p) => true;
        private void _onAddItemCommandExcuted(object p)
        {
            if (_genreOrTag != 0 || !string.IsNullOrEmpty(_title))
            {
                if (_genreOrTag == 1)
                    CourseworkEntities.Instance.Genre.Add(new Genre() { Title = _title });
                else
                    CourseworkEntities.Instance.Tag.Add(new Tag() { Title = _title });

                CourseworkEntities.Instance.SaveChanges();
            }
            else
                MessageBox.Show("Заполните поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion
        #endregion
    }
}
