using Microsoft.AspNetCore.Mvc;
using MyCity.DataAccess;
using MyCity.DataAccess.Models;
using MyCity.DataAccess.Utils;

namespace MyCity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    
    public LocationController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost, Route("AddLocation")]
    public async Task<ActionResult> SetLocation(CancellationToken cancellationToken)
    {
        var location = new Location()
        {
            Name = "SomeLocation",
            DateCreated = DateTime.UtcNow,
            Id = Guid.NewGuid()
        };
        
        await _unitOfWork.LocationRepository.AddAsync(location, cancellationToken);

        List<Location> locations = new();
        
        for (int i = 0; i < 3; i++)
        {
            locations.Add(new Location()
            {
                Name = "SomeLocation" + i,
                DateCreated = DateTime.UtcNow,
                Id = Guid.NewGuid()
            });
        }
        
        await _unitOfWork.LocationRepository.AddRangeAsync(locations, cancellationToken);
        await _unitOfWork.SaveEntitiesAsync(cancellationToken);
        
        return Ok();
    }
}