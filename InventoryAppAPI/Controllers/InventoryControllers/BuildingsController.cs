using InventoryAppAPI.Controllers.InventoryControllers.Abstract;
using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using InventoryAppAPI.Models.Requests.Add;
using InventoryAppAPI.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAppAPI.Controllers.InventoryControllers
{
    public class BuildingsController : InventoryAppController
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
            BuildingDTO addedBuilding = await _buildingRepository.AddBuildingAsync(request);

            return Created($"api/buildings/{addedBuilding.Id}", addedBuilding);
        }

        [HttpPatch("{buildingId}")]
        public async Task<IActionResult> UpdateBuildingAsync([FromRoute] int buildingId, [FromBody] UpdateBuildingRequest request)
        {
            return Ok(await _buildingRepository.UpdateBuildingAsync(buildingId, request));
        }
    }
}
