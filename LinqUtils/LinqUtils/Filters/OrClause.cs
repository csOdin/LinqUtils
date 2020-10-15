namespace csOdin.LinqUtils.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class OrClause<T> : FilterClause<T>
    {
        private readonly List<AndClause<T>> _andClauses = new List<AndClause<T>>();

        public void Add(params AndClause<T>[] andClauses)
        {
            if (andClauses == null)
            {
                return;
            }

            _andClauses.AddRange(andClauses);
        }

        public override Expression<Func<T, bool>> ToLinqExpression(ParameterExpression parameter)
        {
            if ((Conditions == null || !Conditions.Any()) && (_andClauses == null && !_andClauses.Any()))
            {
                return null;
            }

            var orClauses = new List<Expression>();
            Conditions.ToList().ForEach(condition => orClauses.Add(condition.ToLinqExpression(parameter)));

            var orExpression = orClauses.First();
            orClauses.Skip(1).ToList().ForEach(oc => orExpression = Expression.OrElse(orExpression, oc));

            _andClauses.ForEach(ac => orExpression = Expression.OrElse(orExpression, ac.ToLinqExpression(parameter).Body));

            return Expression.Lambda<Func<T, bool>>(orExpression, parameter);
        }
    }
}