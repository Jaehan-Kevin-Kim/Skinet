using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using System.Linq;
using AutoMapper;

namespace API.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")]
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepository<Product> productsRepo,
            IGenericRepository<ProductBrand> productBrandRepo,
            IGenericRepository<ProductType> productTypeRepo,
            IMapper mapper
            )
        {
            this._productsRepo = productsRepo;
            this._productBrandRepo = productBrandRepo;
            this._productTypeRepo = productTypeRepo;
            this._mapper = mapper;
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
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            // var products = await _productsRepo.ListAllAsync();
            // return Ok(products);
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await _productsRepo.ListAsync(spec); // 이 부분이 database와 connect 해서 data를 가져오는 부분z

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));

            // return products.Select(product => new ProductToReturnDto
            // {
            //     Id = product.Id,
            //     Name = product.Name,
            //     Description = product.Description,
            //     PictureUrl = product.PictureUrl,
            //     Price = product.Price,
            //     ProductBrand = product.ProductBrand.Name,
            //     ProductType = product.ProductType.Name
            // }).ToList(); // 이 부분은 database와 통신을 이미 끝내고 값이 저장되있는 products memory에 접근 해서 해당 값을 ProductToReturnDto로 변환하고 또 list로 만들어서 memory에 저장하는 과정
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
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            /*
            1. var product = await _productsRepo.GetByIdAsync(id);
            // if (product == null)
            // {
            //     return BadRequest("The product is not exist.");
            // }
            // return Ok(product);
            */

            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await _productsRepo.GetEntityWithSpec(spec);

            return _mapper.Map<Product, ProductToReturnDto>(product);


            // return new ProductToReturnDto
            // {
            //     Id = product.Id,
            //     Name = product.Name,
            //     Description = product.Description,
            //     PictureUrl = product.PictureUrl,
            //     Price = product.Price,
            //     ProductBrand = product.ProductBrand.Name,
            //     ProductType = product.ProductType.Name
            // };
            // var productToReturnDto = new ProductToReturnDto
            // {
            //     Id = product.Id,
            //     Name = product.Name,
            //     Description = product.Description,
            //     PictureUrl = product.PictureUrl,
            //     Price = product.Price,
            //     ProductBrand = product.ProductBrand.Name,
            //     ProductType = product.ProductType.Name
            // };

            // if (product == null)
            // {
            //     return BadRequest("The product is not exist.");
            // }
            // return Ok(productToReturnDto);
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
