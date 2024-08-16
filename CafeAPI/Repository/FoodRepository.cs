using CafeAPI.Data;
using CafeAPI.Models;
using CafeAPI.Repository.IRepository;

namespace CafeAPI.Repository
{
    public class FoodRepository : Repository<Food>, IFoodRepository
    {
        private readonly AppDbContext _db;

        public FoodRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        // in here we just have to implement Update.. b/c regular 'Repoistory' already has the other methods..
        public async Task<Food> UpdateAsync(Food entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Foods.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
