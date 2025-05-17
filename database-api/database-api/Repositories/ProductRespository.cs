using database_api.Data;
using database_api.Entities;
using database_api.Interfaces;

namespace database_api.Repositories
{
    public class ProductRespository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRespository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
