using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Buildings.Entities;
using Buildings.Web.Controllers.Repositories.Interfaces;
using Buildings.Web.Models.Requests;
using Buildings.Web.Models.Responses;

namespace Buildings.Web.Controllers.Repositories
{
    public class BuildingRepository : IBuildingRepository
    {
        private static IDictionary<int, BuildingEntity> _buildingData = new Dictionary<int, BuildingEntity>()
        {
            { 1, new BuildingEntity
                {
                    Address = "111 Main Street, San Diego",
                    Id = 1,
                    Name = "Star Base",
                    State = "CA",
                    Zipcode = "92101"
                }
            },
            { 2, new BuildingEntity
                {
                    Address = "837 Sugar Road, Chicago",
                    Id = 2,
                    Name = "Soho",
                    State = "IL",
                    Zipcode = "60606"
                }
            },
            { 3, new BuildingEntity
                {
                    Address = "222 Pika Blvd, Los Angeles",
                    Id = 3,
                    Name = "The Stadium",
                    State = "CA",
                    Zipcode = "92001"
                }
            },
            { 4, new BuildingEntity
                {
                    Address = "625 Elephant Drive, Nashville",
                    Id = 4,
                    Name = "Chocolate Factory",
                    State = "TN",
                    Zipcode = "37011"
                }
            },
            { 5, new BuildingEntity
                {
                    Address = "6098 Oak St, Portland",
                    Id = 5,
                    Name = "Cherry Hills",
                    State = "OR",
                    Zipcode = "97213"
                }
            },
        };
        
        public async Task<IEnumerable<BuildingEntity>> GetAllBuildingsAsync(CancellationToken cancellationToken)
        {
            return await Task.FromResult(_buildingData.Values.ToList());
        }
        
        public async Task<BuildingEntity> CreateBuildingAsync(CreateBuildingRequest request, CancellationToken cancellationToken)
        {
            var nextId = _buildingData.Count + 1;
            _buildingData.Add(nextId, new BuildingEntity
            {
                Address = request.Address,
                Id = nextId,
                Name = request.Name,
                State = request.State,
                Zipcode = request.Zipcode
            });

            _buildingData.TryGetValue(nextId, out var created);

            return await Task.FromResult(created);
        }

        public async Task<bool> DeleteBuildingAsync(int buildingId, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_buildingData.Remove(buildingId));
        }
    }
}