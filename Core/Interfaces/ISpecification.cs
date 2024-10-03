using System;
using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    // List<Expression<Func<T, object>>> Includes { get; }
    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T, object>> OrderByDescending { get; }
    bool IsDistinct { get; }
    // int Take { get; }
    // int Skip { get; }
    // bool IsPagingEnabled { get; }
}

// Create an another interface that is used for the Returning a TResult
// that support for the Select method in the EF Core
public interface ISpecification<T, TResult>: ISpecification<T>
{
    Expression<Func<T, TResult>> Select {get;}
}