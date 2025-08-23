using Core.Entities;

namespace Core.Sprecification;

public class ProductSprecification : BaseSprecification<Product>
{
    public ProductSprecification(string? brand, string? type, string? sort)
         : base(p =>
            (string.IsNullOrWhiteSpace(brand) || p.Brand == brand) &&
            (string.IsNullOrWhiteSpace(type) || p.Type == type))
    {
        switch (sort)
        {
            case "priceasc":
                AddOrderBy(p => p.Price);
                break;
            case "pricedesc":
                AddOrderByDescending(p => p.Price);
                break;
            default:
                AddOrderBy(p => p.Name);
                break;
        }
    }
}
