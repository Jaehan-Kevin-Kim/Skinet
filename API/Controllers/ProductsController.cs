using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        [HttpGet]
        public string GetProducts()
        {
            return "this will be a list of product";
        }

        [HttpGet("{id}")]
        public string GetProduct(int id)
        {
            return "this will return one product";
        }
    }
}