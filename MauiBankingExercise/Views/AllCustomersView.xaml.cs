using MauiBankingExercise.ViewModels;
namespace MauiBankingExercise.Views;

public partial class AllCustomersView : BasePage
{
	private AllCustomersViewModel _vm;
	public AllCustomersView(AllCustomersViewModel vm)
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