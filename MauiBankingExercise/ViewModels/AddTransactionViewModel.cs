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
    [QueryProperty(nameof(SelectedAccount), nameof(SelectedAccount))]
    [QueryProperty(nameof(AccountId), nameof(AccountId))]
    [QueryProperty(nameof(CustomerId), nameof(CustomerId))]
    [QueryProperty(nameof(SelectedTransaction), "SelectedTransaction" )]
    public class AddTransactionViewModel : BaseViewModel
    {

		public ICommand SaveTransactionCommand { get; }
        private IBankService _bds;

        public AddTransactionViewModel(IBankService bds)
        {
            _bds = bds;
            SaveTransactionCommand = new Command(async () => await SaveChanges());
          
        }

        private async Task SaveChanges()
        {
            await _bds.UpdateTransaction(SelectedTransaction);
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
        private int _accountId;

        public int AccountId
        {
            get { return _accountId; }
            set
            {
                _accountId = value;
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



        private Transaction _selectedTransaction = new Transaction();

		public Transaction SelectedTransaction
		{
			get { return _selectedTransaction; }
			set 
			{ 
				_selectedTransaction = value;
				OnPropertyChanged();
			}
		}

		private TransactionType? _selectedTransactionType;

		public TransactionType? SelectedTransactionType
		{
			get { return _selectedTransactionType; }
			set
			{
				_selectedTransactionType = value;
                OnPropertyChanged();

                if (_selectedTransactionType != null && SelectedTransaction != null)
                {

                    SelectedTransaction.TransactionType = _selectedTransactionType;
                    SelectedTransaction.TransactionTypeId = _selectedTransactionType.TransactionTypeId;

                    OnPropertyChanged(nameof(SelectedTransaction));
                }
            }
        }

        private TransactionType _transactionType = new TransactionType();
        

        public TransactionType TransactionType
        {
            get { return _transactionType; }
            set
            {
                _transactionType = value;
                OnPropertyChanged();
            }
        }

        private List<TransactionType> _transactionTypes = new List<TransactionType>();

        public List<TransactionType> TransactionTypes
        {
            get { return _transactionTypes; }
            set
            {
                _transactionTypes = value;
                OnPropertyChanged();
            }
        }



        private void Data()
        {
            var Customer = _bds.GetCustomerByID(CustomerId);
            var TransactionTypes = _bds.GetTransactionTypes();
            

        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            Data();
        }
    }
}
