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
    [QueryProperty(nameof(CustomerId), nameof(CustomerId))]
    [QueryProperty(nameof(AccountId), nameof(AccountId))]

    public class TransactionViewModel : BaseViewModel
    {
        public ICommand AddTransactionCommand { get; }

        private BankingDatabaseService _bds;

        private int _customerId;

        public int CustomerId
        {
            get { return _customerId; }
            set { _customerId = value;
                OnPropertyChanged();
                if (_customerId > 0 && _accountId > 0)
                {
                    GetAccountAndTransactions();
                }
            }
        }

        private int _accountId;

        public int AccountId
        {
            get { return _accountId; }
            set { _accountId = value;
                OnPropertyChanged();
                if (_customerId > 0 && _accountId > 0)
                {
                    GetAccountAndTransactions();
                }
            }
        }

        private Account _selectedAccount;

        public Account SelectedAccount
        {
            get { return _selectedAccount; }
            set { _selectedAccount = value;
                OnPropertyChanged();
                LoadTransactions();
            }
        }

        private decimal _transactionAmount;

        public decimal TransactionAmount
        {
            get { return _transactionAmount; }
            set { _transactionAmount = value;
                OnPropertyChanged();
            }
        }

        private string _selectedTransactionType;

        public string SelectedTransactionType
        {
            get { return _selectedTransactionType; }
            set { _selectedTransactionType = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<TransactionType> _transactionTypes = new ObservableCollection<TransactionType>();

        public ObservableCollection<TransactionType> TransactionTypes
        {
            get { return _transactionTypes; }
            set
            {
                _transactionTypes = value;
                OnPropertyChanged();

            }
        }

        private ObservableCollection<Transaction> _transactions = new ObservableCollection<Transaction>();

        public ObservableCollection<Transaction> Transactions
        {
            get { return _transactions; }
            set 
            { 
                _transactions = value;
                OnPropertyChanged();

            }
        }


        private void GetAccountAndTransactions()
        {
            if (AccountId > 0)
            {

                SelectedAccount = _bds.GetAccountById(AccountId);
            }


        }

        private void LoadTransactions()
        {
            Transactions.Clear();
            if (SelectedAccount?.Transactions != null)
            {

                var sortedTransactions = SelectedAccount.Transactions
                    .OrderByDescending(t => t.TransactionDate);

                foreach (var transaction in sortedTransactions)
                {
                    Transactions.Add(transaction);
                }
            }
        }

        public TransactionViewModel(BankingDatabaseService bds)
        {
            _bds = bds;
            AddTransactionCommand = new Command(AddTransaction);
        }

        private async void AddTransaction(object obj)
        {
            var param = new ShellNavigationQueryParameters()
            {
                {"AccountId", AccountId },
                {"CustomerId", CustomerId },
                {"SelectedAccount", SelectedAccount },
                {"TransactionAmount", TransactionAmount },
                {"SelectedTransactionType", SelectedTransactionType },
                {"Transactions", Transactions }

            };
            await AppShell.Current.GoToAsync($"addtransaction", param);

        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            if (CustomerId > 0 && AccountId > 0 && SelectedAccount == null)
            {
                GetAccountAndTransactions();
            }
        }
    } 
}

