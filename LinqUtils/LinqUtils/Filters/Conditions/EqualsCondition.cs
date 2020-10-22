namespace csOdin.LinqUtils.Filters.Conditions
{
    using System;
    using System.Linq.Expressions;

    public class EqualsCondition<T> : Condition<T>
    {
        private EqualsCondition(Expression<Func<T, object>> property, object value)
            : base(property, value)
        {
        }

        private EqualsCondition(string propertyName, object value)
            : base(propertyName, value)
        {
        }

        public static EqualsCondition<T> Create<TValue>(Expression<Func<T, object>> property, TValue value) => new EqualsCondition<T>(property, value);

        public static EqualsCondition<T> Create<TValue>(string propertyName, TValue value) => new EqualsCondition<T>(propertyName, value);

        internal override Expression<Func<T, bool>> ToLinq(ParameterExpression parameter)
        {
            Expression body = parameter;
            PropertyNameParts.ForEach(namePart => body = Expression.PropertyOrField(body, namePart));
            var conditionExpression = Expression.Equal(body, Expression.Constant(Value));

            return base.GetLamda(conditionExpression, parameter);
        }
    }
}