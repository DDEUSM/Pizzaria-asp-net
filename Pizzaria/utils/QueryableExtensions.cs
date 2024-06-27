using System.Linq.Expressions;

public enum SqlLogicOperator
{
    OR,
    AND
}

public static class QueryableExtensions
{
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> queryable,
        bool condition,
        Expression<Func<T, bool>> predicate
    ){
        if (condition)
        {
            return queryable.Where(predicate);
        }
        return queryable;
    }

    public static IQueryable<T> WhereRange<T, B>(
        this IQueryable<T> queryable,
        List<B> collection,
        Func<B, Expression<Func<T, bool>>> predicateFactory
    ){
        collection.ForEach(item => {
            var predicate = predicateFactory(item);
            queryable.Where(predicate);
        });
        
        return queryable;
    }
}