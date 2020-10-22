namespace csOdin.LinqUtils.Filters.Conditions
{
    using System;
    using System.Linq.Expressions;

    public class LessThanCondition<T> : Condition<T>
    {
        private LessThanCondition(Expression<Func<T, object>> property, object value)
            : base(property, value)
        {
        }

        private LessThanCondition(string propertyName, object value)
            : base(propertyName, value)
        {
        }

        public static LessThanCondition<T> Create<TValue>(Expression<Func<T, object>> property, TValue value) => new LessThanCondition<T>(property, value);

        public static LessThanCondition<T> Create<TValue>(string propertyName, TValue value) => new LessThanCondition<T>(propertyName, value);

        internal override Expression<Func<T, bool>> ToLinq(ParameterExpression parameter)
        {
            Expression body = parameter;
            PropertyNameParts.ForEach(namePart => body = Expression.PropertyOrField(body, namePart));
            var conditionExpression = Expression.LessThan(body, Expression.Constant(Value));

            return base.GetLamda(conditionExpression, parameter);
        }
    }
}