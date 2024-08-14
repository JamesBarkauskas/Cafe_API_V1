using System.ComponentModel.DataAnnotations;

namespace CafeAPI.Models.Dto
{
    public class FoodDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public double Price { get; set; }

    }
}
