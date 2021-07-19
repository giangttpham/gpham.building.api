using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Buildings.Entities;
using Buildings.Web.Models.Requests;
using Buildings.Web.Models.Responses;

namespace Buildings.Web.Repositories.Interfaces
{
    public interface IBuildingRepository
    {
        Task<IEnumerable<BuildingEntity>> GetAllBuildingsAsync(CancellationToken cancellationToken);

        Task<BuildingEntity> CreateBuildingAsync(CreateBuildingRequest request, CancellationToken cancellationToken);

        Task<bool> DeleteBuildingAsync(int buildingId, CancellationToken cancellationToken);

    }
}