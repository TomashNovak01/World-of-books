using System.Windows;
using World_of_books.Models;

namespace World_of_books.Data.Classes
{
    internal class SessionData
    {
        public static User CurrentUser { get; set; }
        public static Window CurrentWindow { get; set; }
    }
}
