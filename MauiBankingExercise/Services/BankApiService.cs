using MauiBankingExercise.Configuration;
using MauiBankingExercise.Exceptions;
using MauiBankingExercise.Interfaces;
using MauiBankingExercise.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiBankingExercise.Services
{
    public class BankApiService : IBankService
    {
        private HttpClient _apiClient;
        private ApplicationSettings _apps;

        private JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, // Handles case mismatches between API and model
            WriteIndented = true
        };

        private HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

        public BankApiService(ApplicationSettings apps)
        {
#if DEBUG
            HttpClientHandler insecureHandler = GetInsecureHandler();
            _apiClient = new HttpClient(insecureHandler);
#else
            _apiClient = new HttpClient();
#endif
            _apps = apps;
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            Uri uri = new Uri($"{_apps.ServiceUrl}/Customers");

        try
        {
           HttpResponseMessage response = await _apiClient.GetAsync(uri);
           if (response.IsSuccessStatusCode)
           {
              string content = await response.Content.ReadAsStringAsync();

              List<Customer>? customer = JsonSerializer.Deserialize<List<Customer>>(content, _jsonSerializerOptions);

              return customer ?? new List<Customer>();
           }

        }
          catch(Exception ex)
          {
             Debug.WriteLine($"Error: {ex.Message}");
             throw new BankApiFailedException("Failed to fetch Customers from API");
          }

          return new List<Customer>();
        }

        public async Task<List<Account>> GetAccountsByCustomerId(int customerId)
        {
            Uri uri = new Uri($"{_apps.ServiceUrl}/Accounts/customer/{customerId}");

            try
            {
                HttpResponseMessage response = await _apiClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Account>? account = JsonSerializer.Deserialize<List<Account>>(content, _jsonSerializerOptions);
                    return account ?? new List<Account>();
                }

            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw new BankApiFailedException("Failed to fetch Accounts from API");
            }

            return new List<Account>();
        }

        public async Task<Customer> GetCustomerByID(int id)
        {
            Uri uri = new Uri($"{_apps.ServiceUrl}/Customers/{id}");
            try
            {
                HttpResponseMessage response = await _apiClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Customer? customer = JsonSerializer.Deserialize<Customer>(content, _jsonSerializerOptions);
                    return customer ?? new Customer();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw new BankApiFailedException("Failed to fetch the Customer from API");
            }

            return new Customer();
        }
        public async Task<Account> GetAccountById(int accountId)
        {
            Uri uri = new Uri($"{_apps.ServiceUrl}/Accounts/{accountId}");
            try
            {
                HttpResponseMessage response = await _apiClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Account? account = JsonSerializer.Deserialize<Account>(content, _jsonSerializerOptions);
                    return account ?? new Account();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw new BankApiFailedException("Failed to fetch the Account from API");
            }

            return new Account();
        }

        public async Task AddTransaction(Transaction transaction)
        {
            Uri uri = new Uri($"{_apps.ServiceUrl}/transactions");

            try
            {
                string jsonContent = JsonSerializer.Serialize(transaction, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _apiClient.PostAsync(uri, content);


                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Adding transaction failed with status: {response.StatusCode}");
                    throw new BankApiFailedException($"Failed to add transaction with Account ID {transaction.AccountId}. Status: {response.StatusCode}");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw new BankApiFailedException($"Failed to add transaction");
            }
        }

        public async Task<List<Transaction>> GetTransactionsByAccountId(int accountId)
        {
            Uri uri = new Uri($"{_apps.ServiceUrl}/Transactions/account/{accountId}");
            try
            {
                HttpResponseMessage response = await _apiClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<Transaction>? transactions = JsonSerializer.Deserialize<List<Transaction>>(content, _jsonSerializerOptions);
                    return transactions ?? new List<Transaction>();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw new BankApiFailedException("Failed to fetch the Transactions from API");
            }
            return new List<Transaction>();
        }

        public async Task<List<TransactionType>> GetTransactionTypes()
        {
            Uri uri = new Uri($"{_apps.ServiceUrl}/Transactions/types");
            try
            {
                HttpResponseMessage response = await _apiClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    List<TransactionType>? transactiontypes = JsonSerializer.Deserialize<List<TransactionType>>(content, _jsonSerializerOptions);
                    return transactiontypes ?? new List<TransactionType>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw new BankApiFailedException("Failed to fetch the Transaction types from API");
            }
            return new List<TransactionType>();
        }

        public async Task UpdateTransaction(Transaction transaction)
        {
            Uri uri = new Uri($"{_apps.ServiceUrl}/{transaction}");
            try
            {
                string jsonContent = JsonSerializer.Serialize(transaction, _jsonSerializerOptions);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _apiClient.PutAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Adding transaction failed with status: {response.StatusCode}");
                    throw new BankApiFailedException($"Failed to add transaction with Account ID {transaction.AccountId}. Status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                throw new BankApiFailedException($"Failed to update transaction");
            }

        }
    }
}
