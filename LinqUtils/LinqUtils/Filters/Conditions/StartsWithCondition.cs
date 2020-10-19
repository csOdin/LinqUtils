namespace csOdin.LinqUtils.Filters.Conditions
{
    using System;
    using System.Linq.Expressions;

    public class StartsWithCondition<T> : Condition<T>
    {
        private StartsWithCondition(Expression<Func<T, object>> property, object value)
            : base(property, value)
        {
        }

        private StartsWithCondition(string propertyName, object value)
            : base(propertyName, value)
        {
        }

        public static StartsWithCondition<T> Create<TValue>(Expression<Func<T, object>> property, TValue value) => new StartsWithCondition<T>(property, value);

        public static StartsWithCondition<T> Create<TValue>(string propertyName, TValue value) => new StartsWithCondition<T>(propertyName, value);

        internal override Expression<Func<T, bool>> ToLinq(ParameterExpression parameter)
        {
            Expression body = parameter;
            PropertyNameParts.ForEach(namePart => body = Expression.PropertyOrField(body, namePart));

            var conditionExpression = Expression.Call(body, "startsWith", null, Expression.Constant(Value));
            return Expression.Lambda<Func<T, bool>>(conditionExpression, parameter);
        }
    }
}