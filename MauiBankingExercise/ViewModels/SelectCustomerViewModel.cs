using MauiBankingExercise.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MauiBankingExercise.Models;
using CommunityToolkit.Mvvm.Input;

namespace MauiBankingExercise.ViewModels
{
    public partial class SelectCustomerViewModel : BaseViewModel
    {
        public ICommand MyButtonCommand { get; set; }

        private BankingDatabaseService _bankingDatabaseService;
        private ObservableCollection<Customer> _customers;

        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
            set
            {
                _customers = value;

                OnPropertyChanged();
            }
        }

        public SelectCustomerViewModel(BankingDatabaseService bankingDatabaseService)
        {
            _bankingDatabaseService = bankingDatabaseService;

            MyButtonCommand = new Command(MyButtonAction);
        }

        private void MyButtonAction(object obj)
        {

        }

        [RelayCommand]
        public async Task CustomerSelected(Customer customer)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                {"Customer", customer }
            };

            await Shell.Current.GoToAsync($"customer", navigationParameter);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();

            Customers = new ObservableCollection<Customer>(_bankingDatabaseService.GetAllCustomers());
        }
    }
}
