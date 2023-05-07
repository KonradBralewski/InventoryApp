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
    public class BuildingsController : ControllerBase
    {
        private readonly IBuildingRepository _buildingRepository;

        public BuildingsController(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocationBuildingsAsync()
        {
            return Ok(await _buildingRepository.GetAllBuildingsAsync());
        }
    }
}
