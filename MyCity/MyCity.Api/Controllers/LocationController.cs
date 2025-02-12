using Microsoft.AspNetCore.Mvc;
using MyCity.DataAccess;
using MyCity.DataAccess.Models;

namespace MyCity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> CreateLocation()
    {
        // добавление данных
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Locations.Add(new Location()
            {
                Name = "test"
            });
            db.SaveChanges();
        }

        return Ok();
    }
}