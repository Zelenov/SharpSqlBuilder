using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Operands;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Extensions
{
    public static class SqlSelectBuilderExtensions
    {
        public static ExistsOperator Exists(this SqlSelectBuilder self) => new ExistsOperator(self);
        public static NotExistsOperator NotExists(this SqlSelectBuilder self) => new NotExistsOperator(self);
        public static CountAggregate Count(this SqlSelectBuilder self) => new CountAggregate(self);
        public static EveryAggregate Every(this SqlSelectBuilder self) => new EveryAggregate(self);
        public static MaxAggregate Max(this SqlSelectBuilder self) => new MaxAggregate(self);
        public static MinAggregate Min(this SqlSelectBuilder self) => new MinAggregate(self);
        public static SumAggregate Sum(this SqlSelectBuilder self) => new SumAggregate(self);

    }
}