using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;
using Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _repo;
    

        public ProductsController(IProductRepository repo)
        {
            this._repo = repo;
        }
        //위와 같이 Repository Interface를 Inject 함.


        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _repo.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _repo.GetProductByIdAsync(id);
            // var product = await _repo.GetProductsAsync();
            if (product == null)
            {
                return BadRequest("The product is not exist.");
            }
            return Ok(product);
        }
    }
}
