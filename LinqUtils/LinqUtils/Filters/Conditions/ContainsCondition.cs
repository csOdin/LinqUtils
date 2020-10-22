namespace csOdin.LinqUtils.Filters.Conditions
{
    using System;
    using System.Linq.Expressions;

    public class ContainsCondition<T> : Condition<T>
    {
        private ContainsCondition(Expression<Func<T, object>> property, object value)
            : base(property, value)
        {
        }

        private ContainsCondition(string propertyName, object value)
            : base(propertyName, value)
        {
        }

        public static ContainsCondition<T> Create<TValue>(Expression<Func<T, object>> property, TValue value) => new ContainsCondition<T>(property, value);

        public static ContainsCondition<T> Create<TValue>(string propertyName, TValue value) => new ContainsCondition<T>(propertyName, value);

        internal override Expression<Func<T, bool>> ToLinq(ParameterExpression parameter)
        {
            Expression body = parameter;
            PropertyNameParts.ForEach(namePart => body = Expression.PropertyOrField(body, namePart));
            var conditionExpression = Expression.Call(body, "contains", null, Expression.Constant(Value));

            return base.GetLamda(conditionExpression, parameter);
        }
    }
}