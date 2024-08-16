using CafeAPI.Models;

namespace CafeAPI.Repository.IRepository
{
    public interface IFoodRepository : IRepository<Food>
    {
        // here we just include the update..
        Task<Food> UpdateAsync(Food entity);
    }
}
