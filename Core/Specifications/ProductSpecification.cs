using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    //input: ProductSpecParams(brand, type, sort), Product, output: true/false
    // logic: check if the Product(Type, Brand,...) is in the List of ProductSpecParams.Types, ProductSpecParams.Brands 
    // solve: check if item is in the list by using Contains method
    public ProductSpecification(ProductSpecParams specParams) : base(x =>
        (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
        (specParams.Types.Count == 0 || specParams.Types.Contains(x.Type)) &&
        (specParams.Brands.Count == 0 || specParams.Brands.Contains(x.Brand)) )
    {
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        switch(specParams.Sort)
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
