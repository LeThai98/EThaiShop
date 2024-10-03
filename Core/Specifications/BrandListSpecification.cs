using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications;

public class BrandListSpecification : BaseSpecification<Product, string>
{
    // public BrandListSpecification(Expression<Func<Product, string>> criteria) : base(criteria)
    // {
    //     // why do we need to add the select method here?
    //     // because we are using the new version of BaseSpecification
    //     // that is inherit the BaseSpecification class and implement the new interface

    //     AddSelect(p => p.Brand);
    //     ApplyDistinct();

    // }

    public BrandListSpecification() : base(null)
    {
        // why do we need to add the select method here?
        // because we are using the new version of BaseSpecification
        // that is inherit the BaseSpecification class and implement the new interface
        
        AddSelect(p => p.Brand);
        ApplyDistinct();

    }
}
