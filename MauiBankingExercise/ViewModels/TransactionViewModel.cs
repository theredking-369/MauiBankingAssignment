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
    public class TransactionViewModel : BaseViewModel
    {
        public ICommand AddTransactionCommand { get; }

        private BankingDatabaseService _bds;

        private int _customerId;

        public int CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        private int _accountId;

        public int AccountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }

        private Account? _selectedAccount;

        public Account? SelectedAccount
        {
            get { return _selectedAccount; }
            set { _selectedAccount = value; }
        }

        private decimal _transactionAmount;

        public decimal TransactionAmount
        {
            get { return _transactionAmount; }
            set { _transactionAmount = value; }
        }

        private string _selectedTransactionType;

        public string SelectedTransactionType
        {
            get { return _selectedTransactionType; }
            set { _selectedTransactionType = value; }
        }

        public ObservableCollection<string> TransactionTypes { get; set; } = new ObservableCollection<string> { "Deposit", "Withdrawal" };
        public ObservableCollection<Transaction> Transactions { get; set; } = new ObservableCollection<Transaction>();

        private void GetAccountAndTransactions()
        {
            if (AccountId > 0)
            {

                var accounts = _bds.GetAccountsByCustomerId(CustomerId);
                SelectedAccount = accounts.FirstOrDefault(x => x.AccountId == AccountId);
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
                {"SelectedTransactionType", SelectedTransactionType }

            };
            await AppShell.Current.GoToAsync($"addtransaction", param);

        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            SelectedAccount = null;
        }
    } 
}

