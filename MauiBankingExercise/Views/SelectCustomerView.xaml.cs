using MauiBankingExercise.Services;
using MauiBankingExercise.ViewModels;

namespace MauiBankingExercise.Views;

public partial class SelectCustomerView : ContentPage
{
	public SelectCustomerView(SelectCustomerViewModel vm)
	{
		InitializeComponent();

		var service = BankingDatabaseService.GetInstance();

		BindingContext = new SelectCustomerViewModel(service);
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

		((SelectCustomerViewModel)BindingContext).OnAppearing();

        AppShell.SetBackgroundColor(this, Color.FromRgb(255, 0, 0));
    }
}