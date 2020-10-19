namespace csOdin.LinqUtils.Extensions
{
    using System;
    using System.Linq.Expressions;

    public static class ExpressionExtensions
    {
        public static MemberExpression AsMemberExpression<TIn, TOut>(this Expression<Func<TIn, TOut>> expr) =>
            (expr.Body as MemberExpression) ??
            (!(expr.Body is UnaryExpression unary) ? null : unary.Operand as MemberExpression);

        public static string ToPropertyFullName<TIn, TOut>(this Expression<Func<TIn, TOut>> expr)
        {
            var member = expr.AsMemberExpression();
            var propertyName = member.Member.Name;

            member = member.Expression as MemberExpression;

            while (member != null)
            {
                propertyName = string.Join(".", member.Member.Name, propertyName);
                member = member.Expression as MemberExpression;
            }

            return propertyName;
        }
    }
}