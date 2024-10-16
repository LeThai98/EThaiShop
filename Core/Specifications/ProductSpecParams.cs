using System;

namespace Core.Specifications;

public class ProductSpecParams
{
    private const int MaxPageSize = 50;
    public int PageIndex { get; set; } = 1;

    private int _pageSize = 5;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    
    private List<string> _brands = [];
    public List<string> Brands
    {
        // get { return brands; }
        get => _brands;

        set { _brands = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList(); }
    }
    
    private List<string> _types = [];
    public List<string> Types
    {
        // get { return types; }
        get => _types;

        // input: string(Angular,React), output: List of Types (['Angular','React'])
        // logic: Convert the input string to List of Types
        //solve: split the string by comma and remove empty entries in the List of Types (Ex: Shoes,T-Shirts => [Shoes,T-Shirts])
        set { _types = value.SelectMany(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList(); }
    }

    public string? Sort { get; set; }

    private string? _search;
    public string? Search
    {
        get => _search ?? "";
        set => _search = value?.ToLower();
    }
}
