using InventoryAppAPI.DAL.Entities.Dicts;

namespace InventoryAppAPI.Models.Responses
{
    public class BuildingDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public BuildingDTO(Building building)
        {
            Id = building.Id;
            Name = building.Name;
        }
    }
}
