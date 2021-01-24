namespace csOdin.LinqUtils.Filters.Clauses
{
    using csOdin.LinqUtils.Filters.Conditions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class OrClause<T> : FilterClause<T>
    {
        private OrClause(params AndClause<T>[] andClauses) => Add(andClauses);

        private OrClause(params Condition<T>[] conditions) => Add(conditions);

        public static OrClause<T> Create<T>(params Condition<T>[] conditions) => new OrClause<T>(conditions);

        public static OrClause<T> Create<T>(params AndClause<T>[] andClauses) => new OrClause<T>(andClauses);

        public OrClause<T> Add(params FilterClause<T>[] clauses)
        {
            base.Add(clauses);
            return this;
        }

        public OrClause<T> Add(params Condition<T>[] conditions)
        {
            base.Add(conditions);
            return this;
        }

        public override Expression<Func<T, bool>> ToLinq(ParameterExpression parameter)
        {
            Expression orExpression = Expression.Constant(false);

            var orClauses = new List<Expression>();
            Conditions.ToList().ForEach(condition => orClauses.Add(condition.ToLinq(parameter).Body));

            orClauses.ToList().ForEach(oc => orExpression = Expression.OrElse(orExpression, oc));

            _filterClauses.ForEach(ac => orExpression = Expression.OrElse(orExpression, ac.ToLinq(parameter).Body));

            return Expression.Lambda<Func<T, bool>>(orExpression, parameter);
        }
    }
}