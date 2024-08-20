using Cafe_Web.Models.Dto;

namespace Cafe_Web.Services.IServices
{
    public interface IFoodService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> CreateAsync<T>(FoodCreateDTO dto);
        Task<T> UpdateAsync<T>(FoodUpdateDTO dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
