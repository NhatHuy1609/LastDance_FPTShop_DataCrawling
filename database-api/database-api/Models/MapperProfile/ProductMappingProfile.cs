using AutoMapper;
using database_api.Dtos.Product;
using database_api.Entities;

namespace database_api.Models.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductResponse>();
        }
    }
}
