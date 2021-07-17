using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Buildings.Entities;
using Buildings.Web.Mappers.Interfaces;
using Buildings.Web.Controllers.Repositories.Interfaces;
using Buildings.Web.Models.Requests;
using Buildings.Web.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Buildings.Web.Controllers
{
    [ApiController]
    [Route("buildings")]
    public class BuildingsController : ControllerBase
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly IMapper<BuildingEntity, BuildingResponse> _buildingMapper;

        public BuildingsController(IBuildingRepository buildingRepository,
            IMapper<BuildingEntity, BuildingResponse> buildingMapper)
        {
            _buildingMapper = buildingMapper;
            _buildingRepository = buildingRepository;
        }
        
        [HttpGet]
        [Route("listAll")]
        public async Task<IEnumerable<BuildingResponse>> GetAllBuildings(CancellationToken cancellationToken)
        {
            // Get all buildings from the database
            var allBuildings = await _buildingRepository.GetAllBuildingsAsync(cancellationToken);

            var results = allBuildings.Select(bd => _buildingMapper.Map((bd)));

            return results;
        }

        [HttpPost]
        [Route("createBuilding")]
        public async Task<BuildingResponse> CreateBuilding(CreateBuildingRequest request, CancellationToken cancellationToken)
        {
            var createdEntity = await _buildingRepository.CreateBuildingAsync(request, cancellationToken);
            return _buildingMapper.Map(createdEntity);
        }

        [HttpDelete]
        [Route("deleteBuilding/{id:int}")]
        public async Task<bool> DeleteBuilding(int id, CancellationToken cancellationToken)
        {
            return await _buildingRepository.DeleteBuildingAsync(id, cancellationToken);
        }
    }
}