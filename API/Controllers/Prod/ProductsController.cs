using API.RequestHelpers;
using Core.Entities.Products;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Prod
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IGenericRepository<Product> repo) : BaseApiController
    {
        

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams specParams)
        {
            var spec = new ProductSpecification(specParams);
            return await CreatePagedResult(repo,spec,specParams.PageSize,specParams.PageSize);
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
            var spec = new BrandListSpecification();
            return Ok(await repo.ListAsync(spec));
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeListSpecification();
            return Ok(await repo.ListAsync(spec)); 
        }
        private bool ProductExists(int id) 
        {
            return repo.Exists(id);
        }
    }
}
