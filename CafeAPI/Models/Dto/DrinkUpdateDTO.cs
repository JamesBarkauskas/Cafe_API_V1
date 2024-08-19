using System.ComponentModel.DataAnnotations;

namespace CafeAPI.Models.Dto
{
    public class DrinkUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
        public string ImageUrl { get; set; }
        public string Details { get; set; }
    }
}
