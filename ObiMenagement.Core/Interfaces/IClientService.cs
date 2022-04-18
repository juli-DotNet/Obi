using ObiMenagement.Core.Common;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Interfaces;

public interface IClientService : ICrudService<Client>
{
    Task<Response<List<ClientContact>>> GetClientContacts(int id);
    
    Task<Response> AddClientContacts(ClientContact model);
    Task<Response> RemoveClientContacts(int clientContactId);
}
