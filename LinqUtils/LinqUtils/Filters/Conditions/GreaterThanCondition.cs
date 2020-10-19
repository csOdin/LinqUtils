namespace csOdin.LinqUtils.Filters.Conditions
{
    using System;
    using System.Linq.Expressions;

    public class GreaterThanCondition<T> : Condition<T>
    {
        private GreaterThanCondition(Expression<Func<T, object>> property, object value)
            : base(property, value)
        {
        }

        private GreaterThanCondition(string propertyName, object value)
            : base(propertyName, value)
        {
        }

        public static GreaterThanCondition<T> Create<TValue>(Expression<Func<T, object>> property, TValue value) => new GreaterThanCondition<T>(property, value);

        public static GreaterThanCondition<T> Create<TValue>(string propertyName, TValue value) => new GreaterThanCondition<T>(propertyName, value);

        internal override Expression<Func<T, bool>> ToLinq(ParameterExpression parameter)
        {
            Expression body = parameter;
            PropertyNameParts.ForEach(namePart => body = Expression.PropertyOrField(body, namePart));

            var conditionExpression = Expression.GreaterThan(body, Expression.Constant(Value));
            return Expression.Lambda<Func<T, bool>>(conditionExpression, parameter);
        }
    }
}