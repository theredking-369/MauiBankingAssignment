using MauiBankingExercise.ViewModels;
using MauiBankingExercise.Services;
namespace MauiBankingExercise.Views;

public partial class AddTransactionView : ContentPage
{
    private AddTransactionViewModel _vm;
	public AddTransactionView(AddTransactionViewModel vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.OnAppearing();
    }
}