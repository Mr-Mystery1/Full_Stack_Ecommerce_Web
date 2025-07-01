using Core.Entities.Products;
using Core.Interfaces;
using Core.Interfaces.Products;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Prod
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IGenericRepository<Product> repo) : ControllerBase
    {
        

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type,string? sort)
        {
            var spec = new ProductSpecification(brand, type);
            var products = await repo.ListAsync(spec);


            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);
            return product is null ? NotFound() : product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            
            repo.Add(product);
            if (await repo.SaveAllAsync())
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            
            return BadRequest(" Problem creating product..");

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id,Product product)
        {

            if (product.Id != id || !ProductExists(id))
                return BadRequest("Can't update this product..");

            repo.Update(product);

            //context.Products.Update(product);
            if(await repo.SaveAllAsync())
                return Ok("Product updated..");

            return BadRequest("Problem updating product ..");

        }
        private bool ProductExists(int id) 
        {
            return repo.Exists(id);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUpdate(int id)
        {
            var product = await repo.GetByIdAsync(id);

            if(product is null)
                return NotFound();
            repo.Remove(product);
            if (await repo.SaveAllAsync())
                return Ok("Product deleted..");

            return BadRequest("Problem deleting product..");


        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            //To do Implement Method
            return Ok();
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            //To do Implement Method
            return Ok(); 
        }
    }
}
