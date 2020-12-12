using System;
using System.Collections.Generic;
using System.Linq;
using SharpSqlBuilder.Entities;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Builders
{

    public sealed class SqlSelectBuilder : SqlSelectBuilderHelper<SqlSelectBuilder>
    {
        public new SqlSelectBuilder Values(params SqlTable[] sqlTables) => base.Values(sqlTables);
        public new SqlSelectBuilder Values(IEnumerable<SqlColumn> sqlColumns) => base.Values(sqlColumns);
        public new SqlSelectBuilder From(params SqlTable[] sqlTables) => base.From(sqlTables);
        public new SqlSelectBuilder Join(JoinType joinType, SqlTable sqlTable, Operator on = null) => base.Join(joinType, sqlTable, on);
        public new SqlSelectBuilder InnerJoin(SqlTable sqlTable, Operator on = null) => base.InnerJoin(sqlTable, on);
        public new SqlSelectBuilder LeftJoin(SqlTable sqlTable, Operator on = null) => base.LeftJoin(sqlTable, on);
        public new SqlSelectBuilder FullOuterJoin(SqlTable sqlTable, Operator on = null) => base.FullOuterJoin(sqlTable, on);
        public new SqlSelectBuilder RightJoin(SqlTable sqlTable, Operator on = null) => base.RightJoin(sqlTable, on);
        public new SqlSelectBuilder Where(params Operator[] operators) => base.Where(operators);
        public new SqlSelectBuilder OrderBy(SqlColumn sqlColumn, OrderDirection direction) => base.OrderBy(sqlColumn, direction);

    }
}