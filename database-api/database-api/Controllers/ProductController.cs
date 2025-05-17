using AutoMapper;
using database_api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace database_api.Controllers
{
    [ApiController]
    [ControllerName("products")]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;

        public ProductController(
            IMapper mapper,
            ILogger<ProductController> logger,
            IProductRepository productRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetProductsAsync()
        {
            var products = _productRepository.GetProductsAsync();

            return Ok(products);
        }

        [HttpGet("[controller]/{id}")]
        public IActionResult GetProductByIdAsync(int id) 
        {
            var product = _productRepository.GetProductByIdAsync(id);

            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProductAsync()
        {
            return Ok();
        }
    }
}
