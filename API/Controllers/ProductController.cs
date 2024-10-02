using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IGenericRepository<Product> repository) : ControllerBase
    {
        // [HttpGet]
        // public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        // {
        //     var products = await repository.ListAllAsync();
        //     return Ok(products);
        // }

        // [HttpGet("{id:int}")]
        // public async Task<ActionResult<Product>> GetProduct(int id)
        // {
        //     var product = await repository.GetByIdAsync(id);
        //     if (product == null) return NotFound();
        //     return Ok(product);
        // }

        //https://localhost:5001/api/product?branch=Angular&type=Shoes&sort=priceAsc
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? branch, string? type, string? sort)
        {
            var spec = new ProductSpecification(branch, type, sort);
            var products = await repository.ListAsync(spec);

            return Ok(products);
        }
    }
}
