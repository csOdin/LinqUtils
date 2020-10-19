namespace csOdin.LinqUtils.Filters.Conditions
{
    using System;
    using System.Linq.Expressions;

    public class ExpressionCondition<T> : Condition<T>
    {
        private ExpressionCondition(Expression<Func<T, bool>> condition)
            : base(condition)
        {
        }

        public static ExpressionCondition<T> Create(Expression<Func<T, bool>> condition) => new ExpressionCondition<T>(condition);
    }
}