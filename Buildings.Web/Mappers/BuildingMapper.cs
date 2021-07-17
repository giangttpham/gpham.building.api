using Buildings.Entities;
using Buildings.Web.Mappers.Interfaces;
using Buildings.Web.Models.Responses;

namespace Buildings.Web.Mappers
{ 
    public class BuildingMapper : IMapper<BuildingEntity, BuildingResponse>
    {
        public BuildingResponse Map(BuildingEntity sourceObj)
        {
            return new()
            {
                Address = sourceObj.Address,
                Id = sourceObj.Id,
                Name = sourceObj.Name,
                State = sourceObj.State,
                Zipcode = sourceObj.Zipcode
            };
        }
    }
}