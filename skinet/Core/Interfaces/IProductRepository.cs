using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductsAsync(string? brands, string? types, string? sort);
    Task<Product?> GetProductByIdAsync(int Id);

    Task<IReadOnlyList<string>> GetBrandsAsync();
    Task<IReadOnlyList<string>> GetTypesAsync();

    void Add(Product product);
    void Update(Product product);
    void Delete(Product product);

    bool ProductExists(int Id);
    Task<bool> SaveChangesAsync();
}
