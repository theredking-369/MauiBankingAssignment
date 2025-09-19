using MauiBankingExercise.Interfaces;
using MauiBankingExercise.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


namespace MauiBankingExercise.Services
{
    public class BankingDatabaseService : IBankService
    {
        private SQLiteConnection _dbConnection;
        
        private static BankingDatabaseService _instance;

        public static BankingDatabaseService GetInstance()
        {
            if(_instance == null)
            {
                _instance = new BankingDatabaseService();
            }
            return _instance;
        }

        

        public string GetDatabasePath()
        {
            string filename = "banking.db";
            string pathToDb = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(pathToDb, filename);
           
        }

        public void DeleteDatabase()
        {
            var dbPath = GetDatabasePath();
            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);
            }
        }

       /* public void SaveData()
        {
            string jsonResult = JsonSerializer.Serialize(_transactions);
            string path = GetDatabasePath();

            File.WriteAllText(path, jsonResult);

        }*/

        public BankingDatabaseService()
        {
            //DeleteDatabase();
            _dbConnection = new SQLiteConnection(GetDatabasePath());
            BankingSeeder.Seed(_dbConnection);
        }

        public List<Account> GetAllAccounts()
        {
            return _dbConnection.Table<Account>().ToList();
        }

        /*public List<Bank> GetAllBanks()
        {
            return _dbConnection.Table<Bank>.ToList();
        }*/

        public Task<List<Customer>> GetAllCustomers()
        {
            return Task.FromResult(_dbConnection.Table<Customer>().ToList());
        }

        public List<Transaction> GetAllTransactions()
        {
            return _dbConnection.Table<Transaction>().ToList();
        }


        

        public Task<Customer> GetCustomerByID(int id)
        {
            return Task.FromResult(_dbConnection.Table<Customer>().FirstOrDefault(x => x.CustomerId == id));
        }

        public Task<List<Account>> GetAccountsByCustomerId(int customerId)
        {
            var accounts = _dbConnection.Table<Account>()
                               .Where(a => a.CustomerId == customerId)
                               .ToList();

            foreach (var account in accounts)
            {
                _dbConnection.GetChildren(account);
            }
            return Task.FromResult(accounts);
        }

        public Task<Account> GetAccountById(int accountId)
        {
            var account = _dbConnection.Table<Account>()
                            .FirstOrDefault(a => a.AccountId == accountId);

            if (account != null)
            {
                _dbConnection.GetChildren(account);
            }

            return Task.FromResult(account);
        }

        public Task<List<Transaction>> GetTransactionsByAccountId(int accountId)
        {
            var transactions = _dbConnection.Table<Transaction>()
                .Where(x => x.AccountId == accountId)
                .ToList();

            foreach(var transaction in transactions)
            {
                _dbConnection.GetChildren(transaction);
            }
            return Task.FromResult(transactions);
        }


        public Task<List<TransactionType>> GetTransactionTypes()
        {
            return  Task.FromResult(_dbConnection.Table<TransactionType>().ToList());
        }

        

        public string GetAccountNumber(int accountId)
        {
            var account = _dbConnection.Table<Account>()
                                       .FirstOrDefault(x => x.AccountId == accountId);

            return account?.AccountNumber; // will return null if not found
        }

        
        /*public Task UpTransaction(Transaction transaction)
        {
           
            if(transaction.TransactionId > 0)
            {
                var uniqueTransaction = GetTransactionById(transaction.TransactionId);
                int pos = _transactions.IndexOf(uniqueTransaction);

                _transactions[pos] = transaction;
            }
            else
            {
                int id = _transactions.Count > 0 ? _transactions.Max(x => x.TransactionId) + 1 : 1;
                transaction.TransactionId = id;
                _transactions.Add(transaction);
            }

            SaveData();
            return Task.CompletedTask();            
        }*/

        public Task AddTransaction(Transaction transaction)
        {
            return Task.FromResult(_dbConnection.Insert(transaction));
            
        }

        public Task UpdateTransaction(Transaction transaction)
        {
            return Task.FromResult(_dbConnection.Update(transaction));
        }
    }
}
