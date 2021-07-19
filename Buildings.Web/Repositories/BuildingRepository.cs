using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Buildings.Entities;
using Buildings.Web.Repositories.Interfaces;
using Buildings.Web.Models.Requests;
using Buildings.Web.Models.Responses;

namespace Buildings.Web.Repositories
{
    public class BuildingRepository : IBuildingRepository
    {
        /*
         * For the purpose of this exercise, the data is stored as an in-memory list of objects.
         * In a production environment, the repository class is responsible for interacting with an actual database using a database client (e.g SqlClient)
         */
        public static readonly IDictionary<int, BuildingEntity> BuildingData = new Dictionary<int, BuildingEntity>()
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
        
        /*
         * In the context of this project, the methods below aren't required to be async since the app is not communicating with an external database.
         * However, I keep these as "async" methods to represent what the actual calls to the database would look like.
         */
        //*************************************//
        
        public async Task<BuildingEntity> CreateBuildingAsync(CreateBuildingRequest request, CancellationToken cancellationToken)
        {
            var nextId = BuildingData.Count + 1;
            BuildingData.Add(nextId, new BuildingEntity
            {
                Address = request.Address,
                Id = nextId,
                Name = request.Name,
                State = request.State,
                Zipcode = request.Zipcode
            });

            BuildingData.TryGetValue(nextId, out var created);

            return await Task.FromResult(created);
        }

        /*
         * This method performs a soft delete action, meaning it sets the IsDeleted flag of the building entity, if existed, to true.
         * This will prevent the building to be displayed in the result set of the GetAllBuildingAsync method below.
         * The building would still persist in the database, which is available for reporting or similar purposes.
         */
        public async Task<bool> DeleteBuildingAsync(int buildingId, CancellationToken cancellationToken)
        {
            BuildingData.TryGetValue(buildingId, out var building);

            if (building == null)
            {
                return await Task.FromResult(false);
            }

            building.IsDeleted = true;
            return await Task.FromResult(true);
        }
        
        /*
         * Returns all buildings that are not soft deleted (i.e any building whose IsDeleted flag is false)
         */
        public async Task<IEnumerable<BuildingEntity>> GetAllBuildingsAsync(CancellationToken cancellationToken)
        {
            return await Task.FromResult(BuildingData.Values.Where(bd => !bd.IsDeleted).ToList().OrderBy(bd => bd.Id));
        }
    }
}