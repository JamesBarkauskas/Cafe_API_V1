using CafeAPI.Models;

namespace CafeAPI.Repository.IRepository
{
    public interface IDrinkRepository : IRepository<Drink>
    {
        Task<Drink> UpdateAsync(Drink entity);
    }
}
