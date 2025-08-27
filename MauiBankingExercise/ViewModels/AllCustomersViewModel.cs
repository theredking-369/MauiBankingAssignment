using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MauiBankingExercise.Services;
using MauiBankingExercise.Models;
using System.Collections.ObjectModel;

namespace MauiBankingExercise.ViewModels
{
    public class AllCustomersViewModel : BaseViewModel
    {
        public ICommand CustomerSelectedCommand { get; set; }
        private BankingDatabaseService _bds;

        private ObservableCollection<Customer> _customers = new ObservableCollection<Customer>();

        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;
                OnPropertyChanged();
            }
        }

        private Customer _selectedCustomer;

        public Customer? SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged();
            }
        }





        public AllCustomersViewModel(BankingDatabaseService bds)
        {
            _bds = bds;
            CustomerSelectedCommand = new Command(CustomerSelected);
        }

        private async void CustomerSelected(object obj)
        {
            if (SelectedCustomer != null)
            {
                var param = new ShellNavigationQueryParameters()
                {
                    { "CustomerId" , SelectedCustomer.CustomerId}
                };
                await AppShell.Current.GoToAsync($"singlecustomer", param);
            }
        }

        private void LoadCustomers()
        {
            try
            {
                var customers = _bds.GetAllCustomers();
                Customers.Clear();

                foreach (var customer in customers)
                {
                    Customers.Add(customer);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading customers: {ex.Message}");
            }
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            LoadCustomers();
            SelectedCustomer = null;
        }
    }
}
