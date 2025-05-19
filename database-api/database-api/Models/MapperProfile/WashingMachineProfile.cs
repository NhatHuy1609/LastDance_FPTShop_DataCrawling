using AutoMapper;
using database_api.Dtos.Product;
using database_api.Dtos.WashingMachine;
using database_api.Entities;

namespace database_api.Models.Mapping
{
   public class WashingMachineProfile : Profile
    {
        public WashingMachineProfile()
        {
            CreateMap<WashingMachine, WashingMachineDto>();
            CreateMap<CreateWashingMachineDto, WashingMachine>();
        
            CreateMap<PaginatedResult<WashingMachine>, PaginatedResult<WashingMachineDto>>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
}
