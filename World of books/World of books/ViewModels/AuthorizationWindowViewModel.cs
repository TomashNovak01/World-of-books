using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using World_of_books.ViewModels.Base;

namespace World_of_books.ViewModels
{
    internal class AuthorizationWindowViewModel : ViewModelBase
    {
        private string _title = "Test";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
    }
}
