using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using InventoryAppAPI.Models.Requests.Add;
using InventoryAppAPI.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAppAPI.Controllers.InventoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin, User")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;

        public LocationsController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        [HttpGet("building/{buildingId}")]
        public async Task<IActionResult> GetLocationsByBuildingIdAsync([FromRoute] int buildingId)
        {
            return Ok(await _locationRepository.GetAllLocationsByBuildingIdAsync(buildingId));
        }

        [HttpPost]

        public async Task<IActionResult> AddLocationAsync(AddLocationRequest request)
        {
            LocationDTO addedLocation = await _locationRepository.AddLocationAsync(request);

            return Created($"api/locations/{addedLocation.Id}", addedLocation);
        }

        [HttpPatch("{locationId}")]
        public async Task<IActionResult> UpdateLocationAsync([FromRoute]int locationId,[FromBody] UpdateLocationRequest request)
        {
            return Ok(await _locationRepository.UpdateLocationAsync(locationId, request));
        }

    }
}
