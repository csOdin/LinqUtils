namespace csOdin.LinqUtils.OrderBy
{
    using csOdin.LinqUtils.Extensions;
    using System.Linq;
    using System.Linq.Expressions;

    internal class OrderedQueriable<T>
    {
        private bool isAddingFirstItem = true;

        internal IOrderedQueryable<T> AddOrderByClause(IQueryable<T> query, string propertyName, bool isAscending)
        {
            IOrderedQueryable<T> returnItem;
            returnItem =
                isAscending ?
                AscendingOrderBy(query, propertyName) :
                DescendingOrderBy(query, propertyName);

            isAddingFirstItem = false;
            return returnItem;
        }

        private IOrderedQueryable<T> AscendingOrderBy(IQueryable<T> query, string propertyName) => isAddingFirstItem
            ? OrderExpression("OrderBy", query, propertyName)
            : OrderExpression("ThenBy", query, propertyName);

        private IOrderedQueryable<T> DescendingOrderBy(IQueryable<T> query, string propertyName) => isAddingFirstItem
                ? OrderExpression("OrderByDescending", query, propertyName)
                : OrderExpression("ThenByDescending", query, propertyName);

        private IOrderedQueryable<T> OrderExpression(string orderByMethodName, IQueryable<T> query, string propertyName)
        {
            var entityType = typeof(T);

            var parameterExpression = Expression.Parameter(entityType, "x");
            var expression = propertyName.ToLinqExpression(parameterExpression);
            var selector = Expression.Lambda(expression, new ParameterExpression[] { parameterExpression });

            var propertyInfo = propertyName.ToPropertyInfo(entityType);

            var enumarableType = typeof(Queryable);
            var method = enumarableType.GetMethods()
                 .Where(m => m.Name == orderByMethodName && m.IsGenericMethodDefinition)
                 .Where(m =>
                 {
                     var parameters = m.GetParameters().ToList();
                     return parameters.Count == 2; // overload that has 2 parameters
                 }).Single();

            var genericMethod = method.MakeGenericMethod(entityType, propertyInfo.PropertyType);

            var newQuery = (IOrderedQueryable<T>)genericMethod
                 .Invoke(genericMethod, new object[] { query, selector });
            return newQuery;
        }
    }
}