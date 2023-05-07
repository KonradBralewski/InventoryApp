using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAppAPI.Controllers.InventoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, User")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;

        public LocationsController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        [HttpGet("building/{buildingId}")]
        public async Task<IActionResult> GetAllLocationsByBuildingIdAsync([FromRoute] int buildingId)
        {
            return Ok(await _locationRepository.GetAllLocationsByBuildingIdAsync(buildingId));
        }
    }
}
