using MauiBankingExercise.ViewModels;
namespace MauiBankingExercise.Views;

public partial class TransactionView : BasePage
{
	private TransactionViewModel _vm;
    public TransactionView(TransactionViewModel vm)
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