using System.ComponentModel.DataAnnotations;

namespace CafeAPI.Models.Dto
{
    public class FoodCreateDTO
    {
        //public int Id { get; set; }  // dont need an id for creating.. bc id gets set automatically

        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string Details { get; set; }
    }
}
