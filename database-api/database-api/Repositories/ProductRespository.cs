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

        public async Task<Product> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return new List<Product>();
        }
    }
}
