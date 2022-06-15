using System.Windows;
using World_of_books.Models;

namespace World_of_books.Data.Classes
{
    internal class SessionData
    {
        public static User CurrentUser { get; set; }

        private static Window _currentWindow;
        public static Window CurrentWindow
        {
            get => _currentWindow;
            set
            {
                if(_currentWindow != null)
                    _currentWindow.Close();
                _currentWindow = value;
            }
        }
    }
}
