using System.Collections.Generic;
using SharpSqlBuilder.Blocks;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operands;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Builders
{
    public class SqlSelectBuilderHelper<TBuilder> : SqlSelectBuilderBase where TBuilder : SqlSelectBuilderBase
    {
        protected new TBuilder Values(params SqlTable[] sqlTables) => (TBuilder)base.Values(sqlTables);
        protected new TBuilder Values(IEnumerable<SqlColumn> sqlColumns) => (TBuilder)base.Values(sqlColumns);
        public new TBuilder Values(IEnumerable<Operand> sqlColumns) => (TBuilder)base.Values(sqlColumns);
        public new TBuilder Value(Operand operand) => (TBuilder)base.Value(operand);
        public new TBuilder Star() => (TBuilder)base.Star();
        public new TBuilder CountStar() => (TBuilder)base.CountStar();
        public new TBuilder CustomSql(string customSql, SqlSelectPosition? type = null) => (TBuilder)base.CustomSql(customSql, type);
        protected new TBuilder From(params SqlTable[] sqlTables) => (TBuilder)base.From(sqlTables);
        protected new TBuilder Join(JoinType joinType, SqlTable sqlTable, Operator on = null) => (TBuilder)base.Join(joinType, sqlTable, on);
        protected new TBuilder InnerJoin(SqlTable sqlTable, Operator on = null) => (TBuilder)base.InnerJoin(sqlTable, on);
        protected new TBuilder LeftJoin(SqlTable sqlTable, Operator on = null) => (TBuilder)base.LeftJoin(sqlTable, on);
        protected new TBuilder FullOuterJoin(SqlTable sqlTable, Operator on = null) => (TBuilder)base.FullOuterJoin(sqlTable, on);
        protected new TBuilder RightJoin(SqlTable sqlTable, Operator on = null) => (TBuilder)base.RightJoin(sqlTable, on);
        protected new TBuilder Where(params Operator[] operators) => (TBuilder)base.Where(operators);
        protected new TBuilder OrderBy(SqlColumn sqlColumn, OrderDirection direction) => (TBuilder)base.OrderBy(sqlColumn, direction);
        public new TBuilder Order(OrderMap orderMap) => (TBuilder)base.Order(orderMap);
        public new TBuilder OrderBy(IEnumerable<OrderBlock> orderBy) => (TBuilder)base.OrderBy(orderBy);
        public new TBuilder LimitBy(Operand limitBy) => (TBuilder)base.LimitBy(limitBy);
        public new TBuilder LimitBy(long? limitBy) => (TBuilder)base.LimitBy(limitBy);
        public new TBuilder LimitBy(SqlFilterItem limitBy) => (TBuilder)base.LimitBy(limitBy);
        public new TBuilder Offset(Operand offset) => (TBuilder)base.Offset(offset);
        public new TBuilder Offset(SqlFilterItem offset) => (TBuilder)base.Offset(offset);
        public new TBuilder Offset(long? offset) => (TBuilder)base.Offset(offset);
    }
}