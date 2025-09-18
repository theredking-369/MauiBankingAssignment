
using MauiBankingExercise.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBankingExercise.Services
{
    public class BankingDatabaseService
    {
        private static BankingDatabaseService _instance;

        public static BankingDatabaseService GetInstance()
        {
            if(_instance == null)
            {
                _instance = new BankingDatabaseService();
            }
            return _instance;
        }

        private SQLiteConnection _dbConnection;

        public string GetDatabasePath()
        {
            string filename = "banking.db";
            string pathToDb = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(pathToDb, filename);
        }

        public BankingDatabaseService()
        {
            _dbConnection = new SQLiteConnection(GetDatabasePath());
            BankingSeeder.Seed(_dbConnection);
        }

        public List<Customer> GetAllCustomers()
        {
            return _dbConnection.Table<Customer>().ToList();
        }

        public Customer? GetCustomerByID(int id)
        {
            return _dbConnection.Table<Customer>().FirstOrDefault(x => x.CustomerId == id);
        }

        public List<Account> GetAccountsByCustomerId(int customerId)
        {
            var accounts = _dbConnection.Table<Account>()
                               .Where(a => a.CustomerId == customerId)
                               .ToList();

            foreach (var account in accounts)
            {
                _dbConnection.GetChildren(account);
            }
            return accounts;
        }

        public Account? GetAccountById(int accountId)
        {
            var account = _dbConnection.Table<Account>()
                            .FirstOrDefault(a => a.AccountId == accountId);

            if (account != null)
            {
                _dbConnection.GetChildren(account);
            }

            return account;
        }

        public List<Transaction> _transactions = new List<Transaction>();
        public List<TransactionType> GetTransactionTypes()
        {
            return  _dbConnection.Table<TransactionType>().ToList();
        }

        public List<Account> GetAccounts()
        {
            return _dbConnection.Table<Account>().ToList();
        }

        public string GetAccountNumber(int accountId)
        {
            var account = _dbConnection.Table<Account>()
                                       .FirstOrDefault(x => x.AccountId == accountId);

            return account?.AccountNumber; // will return null if not found
        }

        public Transaction GetTransactionById(int id)
        {
            var uniqueTransaction = _transactions.Where(x => x.TransactionId == id).FirstOrDefault();
            return uniqueTransaction;
        }
        public void AddTransaction(Transaction transaction)
        {
           
                var uniqueTransaction = GetTransactionById(transaction.TransactionId);
                int pos = _transactions.IndexOf(uniqueTransaction);

                _transactions[pos] = transaction;
            
            
            
            _dbConnection.Insert(transaction);
        }
    }
}
