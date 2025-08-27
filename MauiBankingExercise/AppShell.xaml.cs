using MauiBankingExercise.Views;

namespace MauiBankingExercise
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("allcustomers", typeof(AllCustomersView));
            Routing.RegisterRoute("singlecustomer", typeof(SingleCustomerView));

        }
    }
}
