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

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression) =>
        OrderBy = orderByExpression;
        
    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression) =>
        OrderByDescending = orderByDescExpression;
}

public class BaseSprecification<T, TResult>(Expression<Func<T, bool>>? criteria) : BaseSprecification<T>(criteria), ISpecification<T, TResult>
{
    public Expression<Func<T, TResult>>? Select { get; private set; }

    protected void AddSelect(Expression<Func<T, TResult>> selectExpression) =>
        Select = selectExpression;
}