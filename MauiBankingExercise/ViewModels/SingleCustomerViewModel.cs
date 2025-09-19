using MauiBankingExercise.Interfaces;
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
    [QueryProperty(nameof(CustomerId), "CustomerId")]
    [QueryProperty(nameof(SelectedAccount), "SelectedAccount")]
    public class SingleCustomerViewModel : BaseViewModel
    {
        public ICommand AccountSelectedCommand { get; set; }
        private IBankService _bds;

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
                GetCustomerData();
            }
        }

        private ObservableCollection<Account> _customerAccounts = new ObservableCollection<Account>();

        public ObservableCollection<Account> CustomerAccounts 
        {
            get { return _customerAccounts; }
            set 
            { 
                _customerAccounts = value;
                OnPropertyChanged();
            }
        }

        private Account? _selectedAccount;

        public Account? SelectedAccount
        {
            get { return _selectedAccount; }
            set 
            { 
                _selectedAccount = value;
                OnPropertyChanged();
               
            }
        }

        private async void LoadAccounts()
        {
            var accounts = await _bds.GetAccountsByCustomerId(CustomerId);
            CustomerAccounts.Clear();
            if (accounts != null)
            {
                foreach (var account in accounts)
                {
                    CustomerAccounts.Add(account);
                }
            }
        }
        private void GetCustomerData()
        {
            var Customer = _bds.GetCustomerByID(CustomerId);
            LoadAccounts();
        }

        public SingleCustomerViewModel(IBankService bds)
        {
            _bds = bds;
            AccountSelectedCommand = new Command(AccountSelected);

        }

        private async void AccountSelected(object obj)
        {
            var param = new ShellNavigationQueryParameters()
            {
                {"AccountId", SelectedAccount.AccountId },
                {"CustomerId",CustomerId }

            };
                await AppShell.Current.GoToAsync($"transactions", param); 
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            if (CustomerId > 0 && Customer == null)
            {
                GetCustomerData();
            }
        }
    }
}
