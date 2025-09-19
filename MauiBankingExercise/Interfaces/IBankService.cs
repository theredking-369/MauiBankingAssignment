using MauiBankingExercise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBankingExercise.Interfaces
{
    public interface IBankService
    {
        Task AddTransaction(Transaction transaction);
        Task<Account> GetAccountById(int accountId);
        Task<List<Account>> GetAccountsByCustomerId(int customerId);
        Task<List<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerByID(int id);
        Task<List<Transaction>> GetTransactionsByAccountId(int accountId);
        Task<List<TransactionType>> GetTransactionTypes();
        
        Task UpdateTransaction(Transaction transaction);
    }
}
