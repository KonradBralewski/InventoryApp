using InventoryAppAPI.DAL.Entities.Dicts;
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
    public class RoomsController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;

        public RoomsController(IRoomRepository roomRepository, ILocationRepository locationRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet("building/{buildingId}")]
        public async Task<IActionResult> GetRoomsByBuildingIdAsync([FromRoute] int buildingId)
        {
        
            return Ok(await _roomRepository.GetListByBuildingIdAsync(buildingId));
        }

        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetRoomByRoomIdAsync([FromRoute] int roomId)
        {

            return Ok(await _roomRepository.GetByIdAsync(roomId));
        }

        [HttpPost]
        public async Task<IActionResult> AddRoomAsync([FromBody] AddRoomRequest request)
        {
            Room room = await _roomRepository.AddRoomAsync(request);

            return Created($"api/rooms/{room.Id}", room);
        }

        [HttpPatch("{roomId}")]
        public async Task<IActionResult> UpdateRoomAsync([FromRoute]int roomId, [FromBody] UpdateRoomRequest request)
        {
            return Ok(await _roomRepository.UpdateRoomAsync(roomId, request));
        }

    }
}
