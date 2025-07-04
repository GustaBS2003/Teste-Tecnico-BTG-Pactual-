using BTGProject.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BTGProject.Services.Interfaces
{
    public interface IClientService
    {
        ObservableCollection<Client> Clients { get; }
        Task AddClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task DeleteClientAsync(Guid clientId);
    }
}