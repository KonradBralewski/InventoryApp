using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using InventoryAppAPI.Models.Requests.Add;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAppAPI.Controllers.InventoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, User")]
    public class BuildingsController : ControllerBase
    {
        private readonly IBuildingRepository _buildingRepository;

        public BuildingsController(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBuildingsAsync()
        {
            return Ok(await _buildingRepository.GetAllBuildingsAsync());
        }

        [HttpPost]

        public async Task<IActionResult> AddBuildingAsync([FromBody] AddBuildingRequest request)
        {
            Building addedBuilding = await _buildingRepository.AddBuildingAsync(request);

            return Created($"api/buildings/{addedBuilding.Id}", addedBuilding);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateBuildingAsync([FromBody] UpdateBuildingRequest request)
        {
            return Ok(await _buildingRepository.UpdateBuildingAsync(request));
        }
    }
}
