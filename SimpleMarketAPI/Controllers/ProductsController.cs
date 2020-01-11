using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleMarketAPI.Entities;
using SimpleMarketAPI.Models;

namespace SimpleMarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static SIMPLEMARKETContext context = new SIMPLEMARKETContext();
        private static List<Product> lstProduct = context.Products.Select(product => new Product(product.ProductId, product.ProductName, product.ProductDescription, product.ProdcutPrice)).ToList();
        [HttpGet]
        public IActionResult getAll()
        {
           
            return Ok(lstProduct);
        }
       
        [HttpGet("{id}")]
        public IActionResult getAll(string id)
        {
            return Ok(lstProduct.FirstOrDefault(product=>product.productId.Equals(id)));
        }
    }
}