using System.Linq.Expressions;

namespace Core.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T,bool>>? Criteria { get; }
        Expression<Func<T,Object>>? OrderBY { get; }
        Expression<Func<T,Object>>? OrderBYDescending { get; }
        bool IsDistinct {  get; }
    }

    public interface ISpecification<T, TResult> : ISpecification<T>
    {
        Expression<Func<T, TResult>>? Select {  get; }
    }
}
