namespace csOdin.LinqUtils.OrderBy
{
    using System;
    using System.Linq;

    public static class OrderByExtension
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string orderByClause)
        {
            var orderedQueriable = new OrderedQueriable<T>();

            IOrderedQueryable<T> orderBy = null;

            if (string.IsNullOrWhiteSpace(orderByClause))
            {
                return null;
            }

            foreach (var currentClause in orderByClause.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                var clauseParts = currentClause.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var propertyName = clauseParts[0].Trim();
                var ascOrDesc = clauseParts.Length == 2 ? clauseParts[1].Trim().ToLower() : "asc";

                orderBy = orderedQueriable.AddOrderByClause(orderBy ?? query, propertyName, ascOrDesc == "asc");
            }

            return orderBy;
        }
    }
}