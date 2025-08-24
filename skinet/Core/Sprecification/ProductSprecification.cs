using Core.Entities;

namespace Core.Sprecification;

public class ProductSprecification : BaseSprecification<Product>
{
    public ProductSprecification(ProductSprecParams sprecParams)
         : base(p =>
            (!sprecParams.Brands.Any() || sprecParams.Brands.Contains(p.Brand)) &&
            (!sprecParams.Types.Any() || sprecParams.Types.Contains(p.Type)))
    {
        ApplyPaging((sprecParams.PageIndex - 1) * sprecParams.PageSize, sprecParams.PageSize);
        
        switch (sprecParams.Sort)
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
