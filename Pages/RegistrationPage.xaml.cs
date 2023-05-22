using MessengerApp.ViewModels;

namespace MessengerApp.Pages;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage(RegistrationPageViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
    
}