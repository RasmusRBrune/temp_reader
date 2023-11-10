using System.Linq.Expressions;

namespace temperature_Server.Extensions
{

    public static class LINQ
    {
        //public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T,bool>> expression)
        // dunno why had bool condition but commented out
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, Expression<Func<T, bool>> expression)
        {
            if (expression == null)
            {
                return query;
            }
            return query.Where(expression);
        }
    }

}
