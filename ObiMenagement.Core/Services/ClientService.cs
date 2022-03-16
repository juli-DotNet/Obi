using Microsoft.Extensions.Logging;
using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Services;

public class ClientService : BaseService<Client>, IClientService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ILogger logger;

    public ClientService(IUnitOfWork unitOfWork, ILogger<ClientService> logger):base(unitOfWork,unitOfWork.ClientRepository,logger)
    {
        this.unitOfWork = unitOfWork;
        this.logger = logger;
    }
    protected override async Task<bool> ValidateModel(Client model, Response result)
    {
        if (string.IsNullOrWhiteSpace(model.Name))
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Name)));
            return true;
        }

        if (model.Location is null || model.Location.Name is null)
        {
            result.Exception = new ObiException(ErrorMessages.NotNull(nameof(model.Location)));
            return true;
        }
        return false;
    }

}
