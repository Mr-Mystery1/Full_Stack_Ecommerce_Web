using Core.Entities.Products;
using Core.Interfaces.Products;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Prod
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductRepository repo) : ControllerBase
    {
        

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type,string? sort)
        {
            return Ok(await repo.GetProductsAsync(brand, type, sort));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetProductByIdAsync(id);
            return product is null ? NotFound() : product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            
            repo.AddProduct(product);
            if (await repo.SaveChangesAsync())
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            
            return BadRequest(" Problem creating product..");

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id,Product product)
        {

            if (product.Id != id || !ProductExists(id))
                return BadRequest("Can't update this product..");

            repo.UpdateProduct(product);

            //context.Products.Update(product);
            if(await repo.SaveChangesAsync())
                return Ok("Product updated..");

            return BadRequest("Problem updating product ..");

        }
        private bool ProductExists(int id) 
        {
            return repo.ProductExists(id);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUpdate(int id)
        {
            var product = await repo.GetProductByIdAsync(id);

            if(product is null)
                return NotFound();
            repo.DeleteProduct(product);
            if (await repo.SaveChangesAsync())
                return Ok("Product deleted..");

            return BadRequest("Problem deleting product..");


        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            return Ok(await repo.GetBrandsAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            return Ok(await repo.GetTypesAsync());
        }
    }
}
