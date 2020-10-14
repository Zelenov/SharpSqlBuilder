using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Extensions
{
    public static class SqlSelectBuilderExtensions
    {
        public static ExistsOperator Exists(this SqlSelectBuilder self) => new ExistsOperator(self);
        public static NotExistsOperator NotExists(this SqlSelectBuilder self) => new NotExistsOperator(self);
    }
}