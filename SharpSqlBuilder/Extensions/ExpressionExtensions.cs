using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SharpSqlBuilder.Extensions
{
    public static class ExpressionExtensions
    {
        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(this Expression<Func<TSource, TProperty>> action)
        {
            MemberExpression exp;

            switch (action.Body)
            {
                case UnaryExpression unExp when unExp.Operand is MemberExpression operand:
                    exp = operand;
                    break;
                case UnaryExpression _: throw new ArgumentException();
                case MemberExpression body:
                    exp = body;
                    break;
                default: throw new ArgumentException();
            }

            return (PropertyInfo) exp.Member;
        }
    }
}