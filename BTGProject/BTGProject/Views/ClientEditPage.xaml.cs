using BTGProject.ViewModels;

namespace BTGProject.Views;

public partial class ClientEditPage : ContentPage
{
	public ClientEditPage(ClientEditViewModel clientEditViewModel)
	{
		InitializeComponent();
		BindingContext = clientEditViewModel;
    }
}