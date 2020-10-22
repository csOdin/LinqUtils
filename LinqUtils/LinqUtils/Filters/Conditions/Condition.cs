namespace csOdin.LinqUtils.Filters.Conditions
{
    using csOdin.LinqUtils.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class Condition<T>
    {
        private readonly Expression<Func<T, bool>> _expression = null;

        protected Condition(Expression<Func<T, bool>> condition) => _expression = condition;

        protected Condition(Expression<Func<T, object>> property, object value)
        {
            PropertyName = property.ToPropertyFullName();
            Value = value;
        }

        protected Condition(string propertyName, object value)
        {
            PropertyName = propertyName;
            Value = value;
        }

        public string PropertyName { get; private set; }
        public object Value { get; private set; }
        protected bool IsNegation { get; private set; } = false;
        protected List<string> PropertyNameParts => PropertyName.Split('.').ToList();

        public Condition<T> Negate()
        {
            IsNegation = true;
            return this;
        }

        public Expression<Func<T, bool>> ToLinq()
        {
            if (_expression != null)
            {
                return GetLamda(_expression.Body, _expression.Parameters.First());
            }
            var parameter = Expression.Parameter(typeof(T));
            return ToLinq(parameter);
        }

        internal virtual Expression<Func<T, bool>> ToLinq(ParameterExpression parameter) => throw new NotImplementedException();

        protected Expression<Func<T, bool>> GetLamda(Expression conditionExpression, ParameterExpression parameter) => IsNegation
                ? Expression.Lambda<Func<T, bool>>(Expression.Not(conditionExpression), parameter)
                : Expression.Lambda<Func<T, bool>>(conditionExpression, parameter);
    }
}