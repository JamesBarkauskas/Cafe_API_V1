using AutoMapper;
using Cafe_Web.Models.Dto;
using System.Runtime;

namespace Cafe_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<DrinkDTO, DrinkCreateDTO>().ReverseMap();
            CreateMap<DrinkDTO, DrinkUpdateDTO>().ReverseMap();

            CreateMap<FoodDTO, FoodCreateDTO>().ReverseMap();
            CreateMap<FoodDTO, FoodUpdateDTO>().ReverseMap();
        }
    }
}
