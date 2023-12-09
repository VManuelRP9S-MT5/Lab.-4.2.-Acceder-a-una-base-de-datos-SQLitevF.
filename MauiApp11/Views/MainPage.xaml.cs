using MauiApp11.ViewModels;
namespace MauiApp11.Views;


public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        BindingContext = new MainViewModels();
    }
}
