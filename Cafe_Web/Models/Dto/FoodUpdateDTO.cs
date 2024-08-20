using System.ComponentModel.DataAnnotations;

namespace Cafe_Web.Models.Dto
{
    public class FoodUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string Details { get; set; }
    }
}
