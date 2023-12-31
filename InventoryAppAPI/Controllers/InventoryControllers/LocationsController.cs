﻿using InventoryAppAPI.Controllers.InventoryControllers.Abstract;
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
    public class LocationsController : InventoryAppController
    {
        private readonly ILocationRepository _locationRepository;

        public LocationsController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }
        [HttpGet("{locationId}")]
        public async Task<IActionResult> GetLocationByIdAsync([FromRoute] int locationId)
        {
            return Ok(await _locationRepository.GetByIdAsync(locationId));
        }

        [HttpGet("building/{buildingId}")]
        public async Task<IActionResult> GetLocationsByBuildingIdAsync([FromRoute] int buildingId)
        {
            return Ok(await _locationRepository.GetListAsync(l => l.BuildingId == buildingId));
        }

        [HttpGet("room/{roomId}")]
        public async Task<IActionResult> GetLocationsByRoomIdAsync([FromRoute] int roomId)
        {
            return Ok(await _locationRepository.GetListAsync(l => l.RoomNo == roomId));
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
