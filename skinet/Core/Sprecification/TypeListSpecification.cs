using Core.Entities;

namespace Core.Sprecification;

public class TypeListSpecification : BaseSprecification<Product, string>
{
    public TypeListSpecification()
    {
        AddSelect(t => t.Type);
        ApplyDistinct();
    }
}
