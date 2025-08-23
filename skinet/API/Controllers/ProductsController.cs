using Core.Entities;
using Core.Interfaces;
using Core.Sprecification;
using Infrastrucutre.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductsController(IGenericRepository<Product> repo) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProduct(string? brand, string? type, string? sort)
    {
        ProductSprecification spec = new ProductSprecification(brand, type, sort);
        IReadOnlyList<Product> product = await repo.ListAsync(spec);
        return Ok(product);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        Product? product = await repo.GetByIdAsync(id);

        if (product == null) return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repo.Add(product);
        if(await repo.SaveAllAsync())
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        return BadRequest("Failed to create product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !ProductExists(id))
            return BadRequest("Cannot update this poduct.");

        repo.Update(product);
        if (!await repo.SaveAllAsync())
            return BadRequest("Failed to update product");
        return NoContent();
    }
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        Product? product = await repo.GetByIdAsync(id);
        if (product is null) return NotFound();

        repo.Delete(product);
        if (await repo.SaveAllAsync())
            return NoContent();

        return BadRequest("Failed to delete the product");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetProductBrands()
    {
        //TODO: Implement GetBrandsAsync in GenericRepository

        return Ok();
    }
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetProductTypes()
    {
        //TODO: Implement GetTypesAsync in GenericRepository
        return Ok();
    }
    private bool ProductExists(int id)
    {
        return repo.Exists(id);
    }

    
}    
