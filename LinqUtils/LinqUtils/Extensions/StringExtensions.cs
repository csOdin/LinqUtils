namespace csOdin.LinqUtils.Extensions
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class StringExtensions
    {
        public static Expression ToLinqExpression(this string propertyName, ParameterExpression parameterExpression)
        {
            var members = propertyName.Split('.');
            Expression expression = null;
            foreach (var member in members)
            {
                expression = Expression.Property(expression ?? parameterExpression, member);
            }

            return expression;
        }

        public static PropertyInfo ToPropertyInfo(this string propertyName, Type entityType)
        {
            var members = propertyName.Split('.');
            PropertyInfo propertyInfo = null;

            foreach (var member in members)
            {
                propertyInfo = (propertyInfo?.PropertyType ?? entityType).GetProperty(member);
            }

            return propertyInfo;
        }
    }
}