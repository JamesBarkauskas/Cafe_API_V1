using Cafe_Utility;
using Cafe_Web.Models;
using Cafe_Web.Models.Dto;
using Cafe_Web.Services.IServices;

namespace Cafe_Web.Services
{
    public class FoodService : BaseService, IFoodService    // FoodService explicitly defines the methods.. BaseService is the generic methods..
    {
        private readonly IHttpClientFactory _clientFactory;
        private string foodUrl;

        public FoodService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            foodUrl = configuration.GetValue<string>("ServiceUrls:CafeAPI");   
        }

        // now implement each method uisng SendAsync from BaseService
        public Task<T> CreateAsync<T>(FoodCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = foodUrl + "/api/foodAPI"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = foodUrl + "api/foodAPI/" + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = foodUrl + "/api/foodAPI"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = foodUrl + "/api/foodAPI/" + id
            });
        }

        public Task<T> UpdateAsync<T>(FoodUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = foodUrl + "/api/foodAPI/" + dto.Id
            });
        }
    }
}
