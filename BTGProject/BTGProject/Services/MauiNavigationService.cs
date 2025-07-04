using BTGProject.Core.Services.Interfaces;
using BTGProject.Core.ViewModels;
using BTGProject.Views;

namespace BTGProject.Services
{
    public class MauiNavigationService : INavigationService
    {
        public async Task PushModalAsync(ClientEditViewModel viewModel)
        {
            // Crie a página de edição e associe a ViewModel recebida
            var page = new ClientEditPage(viewModel);
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public async Task PopModalAsync()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}