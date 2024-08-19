using CafeAPI.Data;
using CafeAPI.Models;
using CafeAPI.Repository.IRepository;

namespace CafeAPI.Repository
{
    public class DrinkRepository : Repository<Drink>, IDrinkRepository
    {
        private readonly AppDbContext _db;
        public DrinkRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Drink> UpdateAsync(Drink entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Drinks.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
