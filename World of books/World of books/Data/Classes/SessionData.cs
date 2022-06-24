using System.Windows;
using World_of_books.Models;

namespace World_of_books.Data.Classes
{
    internal class SessionData
    {
        #region CurrentUser
        public static User CurrentUser { get; set; }
        #endregion

        #region SelectedUser
        public static User SelectedUser { get; set; }
        #endregion

        #region SelectedBook
        public static Book SelectedBook { get; set; }
        #endregion

        #region SelectedOrder
        public static Order SelectedOrder { get; set; }
        #endregion

        #region CurrentWindow
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
        #endregion

        #region CurrentDialogue
        public static Window CurrentDialogue { get; set; }
        #endregion
    }
}
