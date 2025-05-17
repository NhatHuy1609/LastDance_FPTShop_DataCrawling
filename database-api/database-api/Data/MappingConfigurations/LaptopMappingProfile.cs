using AutoMapper;
using database_api.Dtos.Laptop;
using database_api.Entities;
using database_api.Models;

namespace database_api.Data.MappingConfigurations
{
    public class LaptopMappingProfile : Profile
    {
        public LaptopMappingProfile()
        {
            CreateMap<Laptop, LaptopDto>();
            CreateMap<CreateLaptopDto, Laptop>();
            
            CreateMap<PaginatedResult<Laptop>, PaginatedResult<LaptopDto>>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
} 