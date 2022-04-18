using ObiMenagement.Core.Common;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Interfaces;

public interface IRoadExpenseService
{
    Task<Response> Create(RoadExpense roadData);
    Task<Response> Update(RoadExpense roadData);
    Task<Response> Delete(long id);
    Task<Response<RoadExpense>> GetById(long roadDataId);
    Task<Response<List<RoadExpense>>> GetAll(long tripId);
    Task<Response<List<RoadExpense>>> GetAllByRoad(long roadDataId);
}