namespace csOdin.LinqUtils.OrderBy.Clauses
{
    using csOdin.LinqUtils.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class OrderByClause<T>
    {
        private readonly List<OrderByClausePart> _clauses = new List<OrderByClausePart>();

        private OrderByClause()
        {
        }

        public static OrderByClause<T> Create() => new OrderByClause<T>();

        public OrderByClause<T> AddAscending(Expression<Func<T, object>> property) => AddAscending(property.ToPropertyFullName());

        public OrderByClause<T> AddAscending(string propertyName)
        {
            _clauses.Add(new AscendingOrderByClausePart() { PropertyName = propertyName });
            return this;
        }

        public OrderByClause<T> AddDescending(Expression<Func<T, object>> property) => AddDescending(property.ToPropertyFullName());

        public OrderByClause<T> AddDescending(string propertyName)
        {
            _clauses.Add(new DescendingOrderByClausePart() { PropertyName = propertyName });
            return this;
        }

        public override string ToString() => string.Join(", ", _clauses);
    }
}