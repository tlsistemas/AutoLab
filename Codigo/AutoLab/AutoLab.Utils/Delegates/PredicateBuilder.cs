using System.Linq.Expressions;

namespace AutoLab.Utils.Delegates
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> New<T>(bool defaultExpression = true) => _ => defaultExpression;

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> x, Expression<Func<T, bool>> y) => Expression.Lambda<Func<T, bool>>(Expression.OrElse(x.Body, GetBody(x, y)), x.Parameters);

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> x, Expression<Func<T, bool>> y) => Expression.Lambda<Func<T, bool>>(Expression.AndAlso(x.Body, GetBody(x, y)), x.Parameters);

        private static Expression GetBody<T>(Expression<Func<T, bool>> x, Expression<Func<T, bool>> y) => new RebindParameterVisitor(y.Parameters[0], x.Parameters[0]).Visit(y.Body);

        private class RebindParameterVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _x, _y;

            public RebindParameterVisitor(ParameterExpression x, ParameterExpression y) { _x = x; _y = y; }

            protected override Expression VisitParameter(ParameterExpression node) => node == _x ? _y : base.VisitParameter(node);
        }
    }
}
