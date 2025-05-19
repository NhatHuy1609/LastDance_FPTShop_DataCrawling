using AutoMapper;
using database_api.Dtos.Monitor;
using database_api.Models;

namespace database_api.Data.MappingConfigurations
{
    public class MonitorMappingProfile : Profile
    {
        public MonitorMappingProfile()
        {
            CreateMap<Entities.Monitor, MonitorDto>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price ?? 0))
                .ForMember(dest => dest.PriceDiscount, opt => opt.MapFrom(src => src.PriceDiscount ?? 0));

            CreateMap<CreateMonitorRequest, Entities.Monitor>()
                .ForMember(dest => dest.isAvailable, opt => opt.MapFrom(src => src.IsAvailable.ToString() ?? "true"));

            CreateMap<PaginatedResult<Entities.Monitor>, PaginatedResult<MonitorDto>>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
} 