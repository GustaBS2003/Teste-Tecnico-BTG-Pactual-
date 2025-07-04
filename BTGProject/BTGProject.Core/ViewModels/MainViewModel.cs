using System.Collections.ObjectModel;
using BTGProject.Core.Models;
using BTGProject.Core.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using BTGProject.Core.Services;

namespace BTGProject.Core.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IClientService _clientService;
        private readonly INavigationService _navigationService;
        private readonly IAlertService _alertService;
        private readonly AppSettings _appSettings = new AppSettings();

        [ObservableProperty]
        private ObservableCollection<Client> clients;

        [ObservableProperty]
        private Client selectedClient;

        public MainViewModel(
            IClientService clientService,
            INavigationService navigationService,
            IAlertService alertService)
        {
            _clientService = clientService;
            _navigationService = navigationService;
            _alertService = alertService;
            clients = _clientService.Clients;
        }

        public async Task LoadAsync()
        {
            await _appSettings.LoadAsync();
            _clientService.Clients.Clear();
            foreach (var client in _appSettings.Clients)
                _clientService.Clients.Add(client);
        }

        [RelayCommand]
        private async Task AddClient()
        {
            var editVm = new ClientEditViewModel(_alertService);
            editVm.OnSaved = async (client) =>
            {
                await _clientService.AddClientAsync(client);
                _appSettings.Clients = _clientService.Clients.ToList();
                await _appSettings.SaveAsync();
                await _navigationService.PopModalAsync();
            };
            editVm.OnCanceled = async () =>
            {
                await _navigationService.PopModalAsync();
            };
            await _navigationService.PushModalAsync(editVm);
        }

        [RelayCommand(CanExecute = nameof(CanEditOrDelete))]
        private async Task EditClient()
        {
            if (SelectedClient == null) return;
            var editVm = new ClientEditViewModel(_alertService, SelectedClient);
            editVm.OnSaved = async (client) =>
            {
                await _clientService.UpdateClientAsync(client);
                _appSettings.Clients = _clientService.Clients.ToList();
                await _appSettings.SaveAsync();
                await _navigationService.PopModalAsync();
            };
            editVm.OnCanceled = async () =>
            {
                await _navigationService.PopModalAsync();
            };
            await _navigationService.PushModalAsync(editVm);
        }

        [RelayCommand(CanExecute = nameof(CanEditOrDelete))]
        private async Task DeleteClient()
        {
            if (SelectedClient == null) return;
            if (await _alertService.ShowAlertAsync(
                "Confirm Delete",
                $"Delete client {SelectedClient.Name} {SelectedClient.Lastname}?",
                "Yes", "No"))
            {
                await _clientService.DeleteClientAsync(SelectedClient.Id);
                _appSettings.Clients = _clientService.Clients.ToList();
                await _appSettings.SaveAsync();
                SelectedClient = null;
            }
        }

        private bool CanEditOrDelete() => SelectedClient != null;
    }
}