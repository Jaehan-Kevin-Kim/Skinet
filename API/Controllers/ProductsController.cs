using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly StoreContext _context;
        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync(); // 이 명령어는  ToList()라는 함수를 통해서 db에서 query가 실행 되고, 그 결과값이 products에 저장되는 형태임, 이런 코드는 삽입 후 다시 db update등을 할 필요없이 그냥 server만 재 실행 시키면 됨. (dotnet watch run 해두면 변경사항 있을때마다 자동으로 업데이트 됨)
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return BadRequest("The product is not exist.");
            }
            return Ok(product);
        }
    }
}
