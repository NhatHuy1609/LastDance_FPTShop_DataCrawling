using AutoMapper;
using database_api.Dtos.GamingGear;
using database_api.Entities;
using database_api.Models;

namespace database_api.Data.MappingConfigurations
{
    public class GamingGearMappingProfile : Profile
    {
        public GamingGearMappingProfile()
        {
            CreateMap<GamingGear, GamingGearDto>();
            CreateMap<CreateGamingGearDto, GamingGear>();

            CreateMap<PaginatedResult<GamingGear>, PaginatedResult<GamingGearDto>>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
                
            CreateMap<PagedResult<GamingGear>, PagedResult<GamingGearDto>>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
                .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
                .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(src => src.TotalItems))
                .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(src => src.TotalPages));
        }
    }
}
