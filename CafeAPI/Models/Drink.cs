﻿namespace CafeAPI.Models
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
        public string ImageUrl { get; set; }
        public string Details { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
