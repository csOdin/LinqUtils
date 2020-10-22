namespace csOdin.LinqUtils.Filters.Conditions
{
    using System;
    using System.Linq.Expressions;

    public class EndsWithCondition<T> : Condition<T>
    {
        private EndsWithCondition(Expression<Func<T, object>> property, object value)
            : base(property, value)
        {
        }

        private EndsWithCondition(string propertyName, object value)
            : base(propertyName, value)
        {
        }

        public static EndsWithCondition<T> Create<TValue>(Expression<Func<T, object>> property, TValue value) => new EndsWithCondition<T>(property, value);

        public static EndsWithCondition<T> Create<TValue>(string propertyName, TValue value) => new EndsWithCondition<T>(propertyName, value);

        internal override Expression<Func<T, bool>> ToLinq(ParameterExpression parameter)
        {
            Expression body = parameter;
            PropertyNameParts.ForEach(namePart => body = Expression.PropertyOrField(body, namePart));
            var conditionExpression = Expression.Call(body, "endsWith", null, Expression.Constant(Value));

            return base.GetLamda(conditionExpression, parameter);
        }
    }
}