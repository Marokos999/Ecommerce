using System;
using Core.Entities;
using Core.Interfaces;
using Infrastrucutre.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastrucutre;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext _context;
    public ProductRepository(StoreContext context)
    {
        _context = context;
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
    }

    public void Delete(Product product)
    {
        _context.Products.Remove(product);
    }

    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        return await _context.Products
            .Select(p => p.Brand)
            .Distinct()
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int Id)
    {
       return await _context.Products.FindAsync(Id);
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort )
    {
        IQueryable<Product> query = _context.Products;

        if(!string.IsNullOrEmpty(brand))
            query = query.Where(p => p.Brand == brand);
        
        if(!string.IsNullOrEmpty(type))
            query = query.Where(p => p.Type == type);

        if (!string.IsNullOrEmpty(sort))
        {
            query = sort switch
            {
                "priceasc" => query.OrderBy(p => p.Price),
                "pricedesc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(p => p.Name)
            };
        }
        else
        {
            query = query.OrderBy(p => p.Name);
        }

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
         return await _context.Products
            .Select(p => p.Type)
            .Distinct()
            .ToListAsync();
    }

    public bool ProductExists(int Id)
    {
        return _context.Products.Any(p => p.Id == Id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
    }
}
