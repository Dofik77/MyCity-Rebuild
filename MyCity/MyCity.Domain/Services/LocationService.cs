using MyCity.DataAccess.Models;
using MyCity.Domain.ContractModels;
using MyCity.Domain.Interfaces;

namespace MyCity.Domain.Services;

public class LocationService : ILocationService
{
    // CRUD

    public async Task<Guid> AddLocationAsync(LocationDto location)
    {
        
    }
}