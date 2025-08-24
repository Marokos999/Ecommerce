using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Sprecification;

public class BaseSprecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{
    protected BaseSprecification() : this(null)
    {
    }
    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, object>>? OrderBy { get; private set; }

    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public bool IsDistinct { get; private set; }

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression) =>
        OrderBy = orderByExpression;

    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression) =>
        OrderByDescending = orderByDescExpression;

    protected void ApplyDistinct() => IsDistinct = true;
    
    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
    }
}

public class BaseSprecification<T, TResult>(Expression<Func<T, bool>>? criteria) : BaseSprecification<T>(criteria), ISpecification<T, TResult>
{
     protected BaseSprecification() : this(null!)
    {
    }
    public Expression<Func<T, TResult>>? Select { get; private set; }

    protected void AddSelect(Expression<Func<T, TResult>> selectExpression) =>
        Select = selectExpression;
}