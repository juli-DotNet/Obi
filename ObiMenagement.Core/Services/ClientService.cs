using Microsoft.Extensions.Logging;
using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Services;

public class ClientService : BaseService<Client>, IClientService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger logger;

    public ClientService(IUnitOfWork unitOfWork, ILogger<ClientService> logger) : base(unitOfWork, unitOfWork.ClientRepository, logger)
    {
        this.unitOfWork = unitOfWork;
        this.logger = logger;
    }

    public async Task<Response<List<ClientContact>>> GetClientContacts(int id)
    {
        var result = new Response<List<ClientContact>>();

        try
        {
            result.Result = await _unitOfWork.ClientContactRepository.WhereAsync(a => a.Client.Id == id, a => a.Person, a => a.Client);
        }
        catch (Exception e)
        {
            Logger.Instance.LogError(e);
            result.Exception = e;
        }

        return result;
    }

    public async Task<Response> AddClientContacts(ClientContact model)
    {
        var result = new Response();

        try
        {
            model.Id = 0;
            if (await _unitOfWork.ClientContactRepository.AnyAsync(a => a.Client.Id == model.Client.Id && a.Person.Id == model.Person.Id && a.IsValid))
            {
                result.Exception = new ObiException(ErrorMessages.EntityExist(nameof(model.Person)));
                return result;

            }
            var client = await _repository.FirstOrDefault(a => a.Id == model.Client.Id && a.IsValid);
            if (client is null)
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(model.Client));
                return result;
            }
            model.Client = client;

            var person = await _unitOfWork.PersonRepository.FirstOrDefault(a => a.Id == model.Person.Id && a.IsValid);
            if (person is null)
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(model.Person));
                return result;
            }
            model.Person = person;



            await _unitOfWork.ClientContactRepository.InsertAsync(model);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError("failed to add Client Contact", e);
            Logger.Instance.LogError(e);
            result.Exception = e;
        }

        return result;
    }

    public async Task<Response> RemoveClientContacts(int clientContactId)
    {
        var result = new Response();

        try
        {

            var person = await _unitOfWork.ClientContactRepository.FirstOrDefault(a =>
                a.Id == clientContactId && a.IsValid);
            if (person is null)
            {
                result.Exception = new ObiException(ErrorMessages.EntityDoesntExist(clientContactId));
                return result;
            }

            await _unitOfWork.ClientContactRepository.DeleteAsync(clientContactId);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError("failed to add Client Contact", e);
            Logger.Instance.LogError(e);
            result.Exception = e;
        }

        return result;
    }

    public override async Task<Response<IEnumerable<Client>>> GetAllAsync(string search = null)
    {
        var result = new Response<IEnumerable<Client>>();

        try
        {
            result.Result = await _repository.WhereAsync(a => true, a => a.Location);

        }
        catch (Exception e)
        {
            _logger.LogError(e, "failed to delete the model");
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }
    public override async Task<Response<Client>> GetByIdAsync(int id)
    {
        var result = new Response<Client>();

        try
        {
            result.Result = await _repository.FirstOrDefault(a => a.Id == id, a => a.Location);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "failed to delete the model");
            result.Exception = e;
            Logger.Instance.LogError(e);
        }
        return result;
    }

    protected override async Task<bool> ValidateModel(Client model, Response result)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Name)));
            return true;
        }
        if (model.Location is null || model.Location.Id == 0)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Location)));
            return true;
        }
        model.Location = await unitOfWork.LocationRepository.FirstOrDefault(a => a.Id == model.Location.Id);
        return false;
    }
}
