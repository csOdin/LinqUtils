namespace csOdin.LinqUtils.Filters.Conditions
{
    using System;
    using System.Linq.Expressions;

    public class LessThanOrEqualCondition<T> : Condition<T>
    {
        private LessThanOrEqualCondition(Expression<Func<T, object>> property, object value)
            : base(property, value)
        {
        }

        private LessThanOrEqualCondition(string propertyName, object value)
            : base(propertyName, value)
        {
        }

        public static LessThanOrEqualCondition<T> Create<TValue>(Expression<Func<T, object>> property, TValue value) => new LessThanOrEqualCondition<T>(property, value);

        public static LessThanOrEqualCondition<T> Create<TValue>(string propertyName, TValue value) => new LessThanOrEqualCondition<T>(propertyName, value);

        internal override Expression<Func<T, bool>> ToLinq(ParameterExpression parameter)
        {
            Expression body = parameter;
            PropertyNameParts.ForEach(namePart => body = Expression.PropertyOrField(body, namePart));

            var conditionExpression = Expression.LessThanOrEqual(body, Expression.Constant(Value));
            return Expression.Lambda<Func<T, bool>>(conditionExpression, parameter);
        }
    }
}