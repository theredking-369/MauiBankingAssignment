using MauiBankingExercise.Models;
using MauiBankingExercise.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiBankingExercise.ViewModels
{
    public class SingleCustomerViewModel : BaseViewModel
    {
        public ICommand AccountSelected { get; }
        private BankingDatabaseService _bds;

        private Customer? _customer;

        public Customer? Customer
        {
            get { return _customer; }
            set 
            { 
                _customer = value;
                OnPropertyChanged();
            }
        }

        private int _customerId;

        public int CustomerId
        {
            get { return _customerId; }
            set 
            { 
                _customerId = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Account> _customerAccounts;

        public ObservableCollection<Account> CustomerAccounts
        {
            get { return _customerAccounts; }
            set 
            { 
                _customerAccounts = value;
                OnPropertyChanged();
            }
        }


    }
}
