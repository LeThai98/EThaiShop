using System;
using System.Linq.Expressions;
using Core.Entities;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<T>: ISpecification<T>
{
    private Expression<Func<T, bool>>? criteria = null;
    protected BaseSpecification(Expression<Func<T, bool>>? criteria)
    {
        this.criteria = criteria;
    }

    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, object>>? OrderBy  { get; private set;} 

    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public bool IsDistinct { get; private set; }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
    }   

    protected void ApplyDistinct()
    {
        IsDistinct = true;
    }
}

// create a new another version for BaseSpecification 
// that is inherit the BaseSpecification class and implement the new interface
// Path: Core/Specifications/ISpecification.cs

public class BaseSpecification<T, TResult>
    : BaseSpecification<T>, ISpecification<T, TResult>
{
    public BaseSpecification(Expression<Func<T, bool>>? criteria) : base(criteria){}
    
    public Expression<Func<T, TResult>> Select { get; private set;}

    protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
    {
        Select = selectExpression;
    }
}