using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using InventoryAppAPI.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAppAPI.Controllers.InventoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, User")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly ILocationRepository _locationRepository;

        public RoomsController(IRoomRepository roomRepository, ILocationRepository locationRepository)
        {
            _roomRepository = roomRepository;
            _locationRepository = locationRepository;
        }

        [HttpGet("building/{buildingId}")]
        public async Task<IActionResult> GetAllRoomsByBuildingIdAsync([FromRoute] int buildingId)
        {
            IEnumerable<LocationDTO> locations = await _locationRepository.GetAllLocationsByBuildingIdAsync(buildingId);
            IEnumerable<Room> rooms = locations.Select(l => l.Room);

            return Ok(rooms);
        }
    }
}
