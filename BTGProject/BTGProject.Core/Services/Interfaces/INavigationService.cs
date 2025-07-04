using BTGProject.Core.ViewModels;

namespace BTGProject.Core.Services.Interfaces
{
    public interface INavigationService
    {
        Task PushModalAsync(ClientEditViewModel viewModel);
        Task PopModalAsync();
    }
}