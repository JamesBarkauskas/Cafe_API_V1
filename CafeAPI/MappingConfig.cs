using AutoMapper;
using CafeAPI.Models;
using CafeAPI.Models.Dto;

namespace CafeAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Food, FoodDTO>().ReverseMap();
            CreateMap<Food, FoodCreateDTO>().ReverseMap();
            CreateMap<Food, FoodUpdateDTO>().ReverseMap();

            CreateMap<Drink, DrinkDTO>().ReverseMap();
            CreateMap<Drink, DrinkCreateDTO>().ReverseMap();
            CreateMap<Drink, DrinkUpdateDTO>().ReverseMap();
        }
    }
}
