using AutoMapper;
using database_api.Dtos.Television;
using database_api.Entities;

namespace database_api.Models.MapperProfile
{
    public class TelevisionMappingProfile : Profile
    {
        public TelevisionMappingProfile()
        {
            CreateMap<Television, TelevisionDto>();
            CreateMap<CreateTelevisionDto, Television>();

            CreateMap<PaginatedResult<Television>, PaginatedResult<TelevisionDto>>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
}
