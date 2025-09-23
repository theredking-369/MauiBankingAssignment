using MauiBankingExercise.Models;
using MauiBankingExercise.Services;
using System;
using MauiBankingExercise.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiBankingExercise.ViewModels
{
    //[QueryProperty(nameof(CustomerId), nameof(CustomerId))]
    [QueryProperty(nameof(AccountId), nameof(AccountId))]

    public class TransactionViewModel : BaseViewModel
    {
        public ICommand AddTransactionCommand { get; }

        private IBankService _bds;

        

        private int _accountId;

        public int AccountId
        {
            get { return _accountId; }
            set { _accountId = value;
                OnPropertyChanged();
                
            }
        }

        private Account _selectedAccount;

        public Account SelectedAccount
        {
            get { return _selectedAccount; }
            set { _selectedAccount = value;
                OnPropertyChanged(nameof(SelectedAccount));

                
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

        private Transaction? _selectedTransaction;

        public Transaction? SelectedTransaction
        {
            get { return _selectedTransaction; }
            set 
            { 
                _selectedTransaction = value;
                OnPropertyChanged(nameof(SelectedTransaction));
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

                var SelectedAccount = _bds.GetAccountById(AccountId);
            }


        }

        private async void LoadTransactions()
        {
            var transactions = await _bds.GetTransactionsByAccountId(AccountId);
            Transactions.Clear();
            foreach (var transaction in transactions)
            {
                Transactions.Add(transaction);
            }
        }

        public TransactionViewModel(IBankService bds)
        {
            _bds = bds;
            AddTransactionCommand = new Command(AddTransaction);
        }

        private async void AddTransaction(object obj)
        {
            try
            {

                //create empty transaction
                var newTransaction = new Transaction
                {

                    TransactionId = 1,
                    AccountId = AccountId,
                    TransactionDate = DateTime.Now,
                    Amount = 1,
                    Description = "New Transaction",

                };
                await _bds.AddTransaction(newTransaction);
                var param = new ShellNavigationQueryParameters()
            {
                {"SelectedTransaction", newTransaction }

            };

                await AppShell.Current.GoToAsync($"addtransaction", param);
            }
            catch(Exception ex)
            {

            }
        }

       
        public override void OnAppearing()
        {
            
            base.OnAppearing();
            
            LoadTransactions();
            
        }
    } 
}

