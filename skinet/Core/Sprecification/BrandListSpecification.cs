using Core.Entities;

namespace Core.Sprecification;

public class BrandListSpecification: BaseSprecification<Product, string>
{
    public BrandListSpecification()
    {
        AddSelect(b => b.Brand);
        ApplyDistinct();
    }
}
