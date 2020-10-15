namespace csOdin.LinqUtils.Extensions
{
    using System;
    using System.Linq.Expressions;

    public static class ExpressionExtensions
    {
        public static string ToPropertyFullName<TIn, TOut>(this Expression<Func<TIn, TOut>> expr)
        {
            MemberExpression me;
            me = (MemberExpression)expr.Body;

            var propertyName = me.Member.Name;
            me = me.Expression as MemberExpression;
            while (me != null)
            {
                propertyName = string.Join(".", me.Member.Name, propertyName);
                me = me.Expression as MemberExpression;
            }

            return propertyName;
        }
    }
}