using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(string? branch, string? type, string? sort) : base(x =>
        (string.IsNullOrEmpty(type) || x.Type  == type) &&
        (string.IsNullOrEmpty(branch) || x.Brand == branch) )
    {
        switch(sort)
        {
            case "priceAsc":
                AddOrderBy(p => p.Price);
                break;
            case "priceDesc":
                AddOrderByDescending(p => p.Price);
                break;
            default:
                AddOrderBy(p => p.Name);
                break;
        }
    }
}
