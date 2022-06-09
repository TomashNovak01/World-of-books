using System.Windows;
using World_of_books.ViewModels.AuthorizationAndRegistration;

namespace World_of_books.Views.Windows.Authorization
{
    public partial class AuthorizationAndRegistrationWindow : Window
    {
        public AuthorizationAndRegistrationWindow()
        {
            InitializeComponent();
            AuthorAndRegWindowViewModel.MainFrame = mainFrame;
        }
    }
}
