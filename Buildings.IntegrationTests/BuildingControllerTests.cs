using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Buildings.Web.Repositories;
using Buildings.Web.Models.Requests;
using Buildings.Web.Models.Responses;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Buildings.IntegrationTests
{
    public class BuildingControllerTests
    {
        private HttpClient _httpClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000/buildings/"),
                Timeout = TimeSpan.FromSeconds(30)
            };

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")); 
        }
        [Test]
        public async Task GetAllBuildingsTest()
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "listAll");
            using var response = await _httpClient.SendAsync(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var jsonString = await response.Content.ReadAsStringAsync();
            var buildingList = JsonConvert.DeserializeObject<IEnumerable<BuildingResponse>>(jsonString);
            Assert.AreEqual(BuildingRepository.BuildingData.Count, buildingList.Count());
        }
        
        [Test]
        public async Task DeleteBuildingTest_BuildingDoesNotExist()
        {
            var idToDelete = BuildingRepository.BuildingData.Count + 2;
            using var request = new HttpRequestMessage(HttpMethod.Delete, $"deleteBuilding/{idToDelete}");
            using var response = await _httpClient.SendAsync(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var jsonString = await response.Content.ReadAsStringAsync();
            var deleteResponse = JsonConvert.DeserializeObject<bool>(jsonString);
            Assert.AreEqual(false, deleteResponse);
        }
        
        [Test]
        public async Task DeleteBuildingTest_BuildingExistsInList()
        {
            var idToDelete = BuildingRepository.BuildingData.Count - 1;
            
            // Send a Http request to delete a building with idToDelete
            using var request = new HttpRequestMessage(HttpMethod.Delete, $"deleteBuilding/{idToDelete}");
            using var response = await _httpClient.SendAsync(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var jsonString = await response.Content.ReadAsStringAsync();
            var deleteResponse = JsonConvert.DeserializeObject<bool>(jsonString);
            Assert.AreEqual(true, deleteResponse);
            
            // Send a http request to get all buildings and verity that the deleted building was not returned
            using var getRequest = new HttpRequestMessage(HttpMethod.Get, "listAll");
            using var getResponse = await _httpClient.SendAsync(getRequest);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            jsonString = await getResponse.Content.ReadAsStringAsync();
            var buildingList = JsonConvert.DeserializeObject<IEnumerable<BuildingResponse>>(jsonString);
            foreach (var building in buildingList)
            {
                if (building.Id == idToDelete)
                {
                    Assert.Equals(1, 2);
                }
            }
        }
        
        [Test]
        public async Task CreateBuildingTest()
        {
            var createRequest = new CreateBuildingRequest
            {
                Address = "123 Main st, san diego",
                Name = "Candy shop",
                State = "CA",
                Zipcode = "92156",
            };
            // using var request = new HttpRequestMessage(HttpMethod.Post, $"createBuilding");
            var json = JsonConvert.SerializeObject(createRequest);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var response = await _httpClient.PostAsync("createBuilding", data);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var jsonString = await response.Content.ReadAsStringAsync();
            var createResponse = JsonConvert.DeserializeObject<BuildingResponse>(jsonString);
            Assert.AreEqual(createRequest.Address, createResponse.Address);
            Assert.AreEqual(createRequest.Name, createResponse.Name);
            Assert.AreEqual(createRequest.State, createResponse.State);
            Assert.AreEqual(createRequest.Zipcode, createResponse.Zipcode);
        }
    }
}