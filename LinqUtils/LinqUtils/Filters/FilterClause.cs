namespace csOdin.LinqUtils.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public abstract class FilterClause<T>
    {
        public FilterClause() => Conditions = new List<Condition<T>>();

        protected List<Condition<T>> Conditions { get; }

        public void Add(params Condition<T>[] conditions)
        {
            if (conditions == null)
            {
                return;
            }
            Conditions.AddRange(conditions);
        }

        public abstract Expression<Func<T, bool>> ToLinqExpression(ParameterExpression parameter);

        public Expression<Func<T, bool>> ToLinqExpression()
        {
            var parameter = Expression.Parameter(typeof(T));
            return ToLinqExpression(parameter);
        }
    }
}