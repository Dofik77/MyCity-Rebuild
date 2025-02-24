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
    
    [HttpGet]
    public async Task<ActionResult> CreateLocation(CancellationToken cancellationToken = default)
    {
        // добавление данных
        var location = new Location();
        
        await _unitOfWork.LocationRepository.AddAsync(location, cancellationToken);
        await _unitOfWork.LocationRepository.SaveChangesAsync();
        
        return Ok();
    }
}