using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Interfaces;

public interface ITripService : ICrudService<Trip>
{

    public Task<int> CalculateNumber();
}