using System.Collections.ObjectModel;
using BTGProject.Models;
using BTGProject.Services.Interfaces;
using BTGProject.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BTGProject.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IClientService _clientService;
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty]
        private ObservableCollection<Client> clients;

        [ObservableProperty]
        private Client selectedClient;

        public MainViewModel(IClientService clientService, IServiceProvider serviceProvider)
        {
            _clientService = clientService;
            _serviceProvider = serviceProvider;
            Clients = _clientService.Clients;
        }

        [RelayCommand]
        private async Task AddClient()
        {
            var editVm = new ClientEditViewModel();
            var editPage = new ClientEditPage(editVm);
            await Application.Current.MainPage.Navigation.PushModalAsync(editPage);
            editVm.OnSaved = async (client) =>
            {
                await _clientService.AddClientAsync(client);
                await Application.Current.MainPage.Navigation.PopModalAsync();
            };
            editVm.OnCanceled = async () =>
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            };
        }

        [RelayCommand(CanExecute = nameof(CanEditOrDelete))]
        private async Task EditClient()
        {
            if (SelectedClient == null) return;
            var editVm = new ClientEditViewModel(SelectedClient);
            var editPage = new ClientEditPage(editVm);
            await Application.Current.MainPage.Navigation.PushModalAsync(editPage);
            editVm.OnSaved = async (client) =>
            {
                await _clientService.UpdateClientAsync(client);
                await Application.Current.MainPage.Navigation.PopModalAsync();
            };
            editVm.OnCanceled = async () =>
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            };
        }

        [RelayCommand(CanExecute = nameof(CanEditOrDelete))]
        private async Task DeleteClient()
        {
            if (SelectedClient == null) return;
            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "Confirm Delete",
                $"Delete client {SelectedClient.Name} {SelectedClient.Lastname}?",
                "Yes", "No");
            if (confirm)
            {
                await _clientService.DeleteClientAsync(SelectedClient.Id);
                SelectedClient = null;
            }
        }

        private bool CanEditOrDelete() => SelectedClient != null;
    }
}