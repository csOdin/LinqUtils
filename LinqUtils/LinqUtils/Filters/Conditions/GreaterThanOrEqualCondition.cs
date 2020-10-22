namespace csOdin.LinqUtils.Filters.Conditions
{
    using System;
    using System.Linq.Expressions;

    public class GreaterThanOrEqualCondition<T> : Condition<T>
    {
        private GreaterThanOrEqualCondition(Expression<Func<T, object>> property, object value)
            : base(property, value)
        {
        }

        private GreaterThanOrEqualCondition(string propertyName, object value)
            : base(propertyName, value)
        {
        }

        public static GreaterThanOrEqualCondition<T> Create<TValue>(Expression<Func<T, object>> property, TValue value) => new GreaterThanOrEqualCondition<T>(property, value);

        public static GreaterThanOrEqualCondition<T> Create<TValue>(string propertyName, TValue value) => new GreaterThanOrEqualCondition<T>(propertyName, value);

        internal override Expression<Func<T, bool>> ToLinq(ParameterExpression parameter)
        {
            Expression body = parameter;
            PropertyNameParts.ForEach(namePart => body = Expression.PropertyOrField(body, namePart));
            var conditionExpression = Expression.GreaterThanOrEqual(body, Expression.Constant(Value));

            return base.GetLamda(conditionExpression, parameter);
        }
    }
}