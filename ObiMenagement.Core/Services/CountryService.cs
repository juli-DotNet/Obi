using ObiMenagement.Core.Common;
using ObiMenagement.Core.Interfaces;
using ObiMenagement.Core.Models;

namespace ObiMenagement.Core.Services;

public class CountryService:BaseService,ICountryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CountryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Response> CreateAsync(County model)
    {
       return Run(() => _unitOfWork.CountryRepository.InsertAsync(model).GetAwaiter().GetResult());
        // var result=RunAsync( async () =>
        // { 
        //     if (string.IsNullOrWhiteSpace(model.Name))
        //     {
        //         throw new ObiException(ErrorMessages.NotNull(nameof(model.Name)));
        //     }
        //     if (  _unitOfWork.CountryRepository.AnyAsync(a=>a.Name==model.Name && a.IsValid).GetAwaiter().GetResult())
        //     {
        //         throw new ObiException(ErrorMessages.EntityExist(nameof(model.Name)));
        //     }
        //
        //     _unitOfWork.CountryRepository.InsertAsync(model).GetAwaiter().GetResult();
        //
        // }).GetAwaiter().GetResult();
        // return result;

    }

    public async Task<Response> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Response> EditAsync(County model)
    {
        throw new NotImplementedException();
    }

    public  async Task<Response<IEnumerable<County>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Response<County>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}