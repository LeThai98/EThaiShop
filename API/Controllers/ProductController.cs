using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductController(IGenericRepository<Product> repository) : BaseController
    {
        // [HttpGet]
        // public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        // {
        //     var products = await repository.ListAllAsync();
        //     return Ok(products);
        // }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repository.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        //https://localhost:5001/api/product?branch=Angular&type=Shoes&sort=priceAsc
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] ProductSpecParams productSpecParams)
        {
            var spec = new ProductSpecification(productSpecParams);
           
            return await  CreatePageResult<Product>(repository, spec, productSpecParams.PageIndex, productSpecParams.PageSize);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetBrands()
        {
            var spec = new BrandListSpecification();
            var brands = await repository.ListAsync(spec);

            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetTypes()
        {
            var spec = new TypeListSpecification();
            var types = await repository.ListAsync(spec);

            return Ok(types);
        }
    }
}
