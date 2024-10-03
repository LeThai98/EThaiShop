using System;
using Core.Entities;

namespace Core.Specifications;

// do the same as the BaseSpecification class

public class TypeListSpecification : BaseSpecification<Product, string>
{
    public TypeListSpecification() : base(null)
    {
        AddSelect(p => p.Type);
        ApplyDistinct();
    }
}