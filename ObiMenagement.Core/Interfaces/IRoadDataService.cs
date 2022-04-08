using ObiMenagement.Core.Common;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Interfaces;

public interface IRoadDataService 
{
    Task<Response> Create(RoadData roadData,long tripId);
    Task<Response> Update(RoadData roadData, long tripId);
    Task<Response> Delete(long roadDataId);
    Task<Response<RoadData>> GetById(long roadDataId);
    Task<Response<List<RoadData>>> GetAll(long tripId);
}