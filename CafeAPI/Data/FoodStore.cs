using CafeAPI.Models;
using CafeAPI.Models.Dto;

namespace CafeAPI.Data
{
    public class FoodStore
    {
        public List<FoodDTO> FoodList = new List<FoodDTO>
        {
            new FoodDTO {Id=1, Name="Sandwich", Price=4.99 },
            new FoodDTO {Id=2, Name="Soup", Price=1.99},
            new FoodDTO {Id=3, Name="Muffin", Price = 1.49}
        };
        
    }
}
