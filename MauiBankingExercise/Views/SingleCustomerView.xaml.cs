using MauiBankingExercise.ViewModels;
namespace MauiBankingExercise.Views;

public partial class SingleCustomerView : BasePage
{
	private SingleCustomerViewModel _vm;
	public SingleCustomerView(SingleCustomerViewModel vm)
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