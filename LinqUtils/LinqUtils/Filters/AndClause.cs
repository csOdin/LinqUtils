namespace csOdin.LinqUtils.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class AndClause<T> : FilterClause<T>
    {
        private readonly List<OrClause<T>> _orClauses = new List<OrClause<T>>();

        public void Add(params OrClause<T>[] orClauses)
        {
            if (orClauses == null)
            {
                return;
            }

            _orClauses.AddRange(orClauses);
        }

        public override Expression<Func<T, bool>> ToLinqExpression(ParameterExpression parameter)
        {
            if ((Conditions == null || !Conditions.Any()) && (_orClauses == null || !_orClauses.Any()))
            {
                return null;
            }

            var andClauses = new List<Expression>();
            Conditions.ToList().ForEach(condition => andClauses.Add(condition.ToLinqExpression(parameter)));

            var andExpression = andClauses.First();
            andClauses.Skip(1).ToList().ForEach(ac => andExpression = Expression.AndAlso(andExpression, ac));

            _orClauses.ForEach(oc => andExpression = Expression.AndAlso(andExpression, oc.ToLinqExpression(parameter).Body));

            return Expression.Lambda<Func<T, bool>>(andExpression, parameter);
        }
    }
}