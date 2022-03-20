using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;

        
        public ProductsController(
            IGenericRepository<Product> productsRepo, 
            IGenericRepository<ProductBrand> productBrandRepo, 
            IGenericRepository<ProductType> productTypeRepo)        
        {
            this._productsRepo = productsRepo;
            this._productBrandRepo = productBrandRepo;
            this._productTypeRepo = productTypeRepo;
        }

        // private readonly IProductRepository _repo;


        // public ProductsController(IProductRepository repo)
        // {
        //     this._repo = repo;
        // }
        // //위와 같이 Repository Interface를 Inject 함.



        // [HttpGet]
        // public async Task<ActionResult<List<Product>>> GetProducts()
        // {
        //     var products = await _repo.GetProductsAsync();
        //     return Ok(products);
        // }
        [HttpGet]

        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productsRepo.ListAllAsync();
            return Ok(products);
        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<Product>> GetProduct(int id)
        // {
        //     var product = await _repo.GetProductByIdAsync(id);
        //     if (product == null)
        //     {
        //         return BadRequest("The product is not exist.");
        //     }
        //     return Ok(product);
        // }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productsRepo.GetByIdAsync(id);
            if (product == null)
            {
                return BadRequest("The product is not exist.");
            }
            return Ok(product);
        }

        // [HttpGet("brands")]
        // public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        // {
        //     var productBrands = await _repo.GetProductBrandsAsync();

        //     return Ok(productBrands);
        // }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await _productBrandRepo.ListAllAsync();

            return Ok(productBrands);
        }



        // [HttpGet("types")]
        // public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        // {
        //     // var productTypes = await _repo.GetProductTypesAsync();
        //     // return Ok(productTypes);
        //     return Ok(await _repo.GetProductTypesAsync());

        // }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            // var productTypes = await _repo.GetProductTypesAsync();
            // return Ok(productTypes);
            return Ok(await _productTypeRepo.ListAllAsync());

        }
    }
}
