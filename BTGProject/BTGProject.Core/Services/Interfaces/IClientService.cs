using BTGProject.Core.Models;
using System.Collections.ObjectModel;

namespace BTGProject.Core.Services.Interfaces
{
    public interface IClientService
    {
        ObservableCollection<Client> Clients { get; }
        Task AddClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(Guid clientId);
    }
}