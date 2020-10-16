namespace csOdin.LinqUtils.Filters
{
    using csOdin.LinqUtils.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class Condition<T>
    {
        private readonly Expression<Func<T, bool>> _expression = null;

        public Condition(Expression<Func<T, object>> property, FilterOperators filterOperator, string value)
        {
            Operator = filterOperator;
            PropertyName = property.ToPropertyFullName();
            Value = value;
        }

        public Condition(string propertyName, FilterOperators filterOperator, string value)
        {
            Operator = filterOperator;
            PropertyName = propertyName;
            Value = value;
        }

        public Condition(Expression<Func<T, bool>> condition) => _expression = condition;

        public FilterOperators Operator { get; private set; } = FilterOperators.Contains;

        public string PropertyName { get; private set; }
        public string Value { get; private set; }
        private List<string> PropertyNameParts => PropertyName.Split('.').ToList();

        public Expression<Func<T, bool>> ToLinq()
        {
            if (_expression != null)
            {
                return _expression;
            }
            var parameter = Expression.Parameter(typeof(T));
            return ToLinq(parameter);
        }

        internal Expression<Func<T, bool>> ToLinq(ParameterExpression parameter)
        {
            var stringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            Expression body = parameter;
            PropertyNameParts.ForEach(namePart => body = Expression.PropertyOrField(body, namePart));

            var conditionExpression = Expression.Call(body, "contains", null, Expression.Constant(Value));
            return Expression.Lambda<Func<T, bool>>(conditionExpression, parameter);
        }
    }
}