namespace csOdin.LinqUtils.Filters
{
    using csOdin.LinqUtils.Filters.Clauses;
    using csOdin.LinqUtils.Filters.Conditions;
    using System.Linq;

    public static class WhereExtension
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> query, OrClause<T> condition) =>
            query.Where(condition.ToLinq());

        public static IQueryable<T> Where<T>(this IQueryable<T> query, Condition<T> condition) =>
                            query.Where(condition.ToLinq());

        public static IQueryable<T> Where<T>(this IQueryable<T> query, AndClause<T> condition) =>
                            query.Where(condition.ToLinq());
    }
}