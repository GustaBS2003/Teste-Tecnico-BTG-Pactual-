using System.Collections.ObjectModel;
using BTGProject.Core.Models;
using BTGProject.Core.Services.Interfaces;

namespace BTGProject.Core.Services
{
    public class ClientService : IClientService
    {
        private readonly AppSettings _appSettings;
        public ObservableCollection<Client> Clients { get; }

        public ClientService(AppSettings appSettings)
        {
            _appSettings = appSettings;
            Clients = new ObservableCollection<Client>(_appSettings.Clients);
            Clients.CollectionChanged += (s, e) =>
            {
                _appSettings.Clients = Clients.ToList();
            };
        }

        public Task AddClientAsync(Client client)
        {
            Clients.Add(client);
            return Task.CompletedTask;
        }

        public Task UpdateClientAsync(Client client)
        {
            var existing = Clients.FirstOrDefault(c => c.Id == client.Id);
            if (existing != null)
            {
                existing.Name = client.Name;
                existing.Lastname = client.Lastname;
                existing.Age = client.Age;
                existing.Address = client.Address;
            }
            return Task.CompletedTask;
        }

        public Task DeleteClientAsync(Guid clientId)
        {
            var client = Clients.FirstOrDefault(c => c.Id == clientId);
            if (client != null)
                Clients.Remove(client);
            return Task.CompletedTask;
        }
    }
}