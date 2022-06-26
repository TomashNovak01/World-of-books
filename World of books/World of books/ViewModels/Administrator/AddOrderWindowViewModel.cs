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
    internal class AddOrderWindowViewModel : ViewModelBase
    {
        #region Fields
        #region Constants
        public const int CUSTOMER_ROLE = 1;
        public const int ADMINISTRATION_ROLE = 2;
        #endregion

        #region CustomersList
        private List<User> _customersList = new List<User>();
        public List<User> CustomersList
        {
            get => _customersList;
            set => Set(ref _customersList, value);
        }
        #endregion

        #region IdUser
        private int _idUser;
        public int IdUser
        {
            get => _idUser;
            set => Set(ref _idUser, value);
        }
        #endregion

        #region SelectedDate
        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => Set(ref _selectedDate, value);
        }
        #endregion

        #region Price
        private string _price;
        public string Price
        {
            get => _price;
            set => Set(ref _price, value);
        }
        #endregion
        #endregion

        public AddOrderWindowViewModel()
        {
            FillUsersList();
            FillFields();
            SaveChangeDataOrderCommand = new LambdaCommand(_onSaveChangeDataOrderCommandExcuted, _canSaveChangeDataOrderCommandExcute);
        }

        #region Commands
        private void FillUsersList()
        {
            _customersList.Clear();

            _customersList.Add(new User() { Lastname = "(пусто)" });
            foreach (var customer in CourseworkEntities.Instance.User.Where(u => u.IdRole == CUSTOMER_ROLE))
                _customersList.Add(customer);
        }

        private void FillFields()
        {
            if (SessionData.SelectedOrder != null)
            {
                _idUser = SessionData.SelectedOrder.IdUser;
                _selectedDate = SessionData.SelectedOrder.DateOfOrder;
                _price = SessionData.SelectedOrder.OrderPrice.ToString();
            }
        }

        #region SaveChangeDataOrderCommand
        public ICommand SaveChangeDataOrderCommand { get; }
        private bool _canSaveChangeDataOrderCommandExcute(object p) => true;
        private void _onSaveChangeDataOrderCommandExcuted(object p)
        {
            if (CheckData())
            {
                if (SessionData.SelectedOrder == null)
                    CourseworkEntities.Instance.Order.Add(new Order()
                    {
                        IdUser = _idUser,
                        DateOfOrder = _selectedDate,
                        OrderPrice = Convert.ToDecimal(_price)
                    });

                else
                {
                    SessionData.SelectedOrder.IdUser = _idUser;
                    SessionData.SelectedOrder.DateOfOrder = _selectedDate;
                    SessionData.SelectedOrder.OrderPrice = Convert.ToDecimal(_price);
                }

                CourseworkEntities.Instance.SaveChanges();
                SessionData.CurrentDialogue.Close();
            }
            else
                MessageBox.Show("Заполните корректно поля", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        private bool CheckData()
        {
            if (_idUser == 0)
                return false;

            if (string.IsNullOrEmpty(_price) || !decimal.TryParse(_price, out decimal dResult))
                return false;

            if (_selectedDate == null || _selectedDate == DateTime.MinValue)
                return false;
            return true;
        }
        #endregion
        #endregion
    }
}
