using System;
using System.Collections.Generic;
using System.Text;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Builders
{
    public sealed class SqlInsertBuilder : SqlInsertBuilderHelper<SqlInsertBuilder>
    {
        public SqlInsertBuilder(SqlTable sqlTable) : base(sqlTable)
        {
        }
        public new SqlInsertBuilder Values(IEnumerable<SqlColumn> sqlColumns) => base.Values(sqlColumns);
        public new SqlInsertBuilder OnConflict(IEnumerable<SqlColumn> sqlColumns) => base.OnConflict(sqlColumns);
        public new SqlInsertBuilder DoUpdate(IEnumerable<SqlColumn> sqlColumns) => base.DoUpdate(sqlColumns);
        public new SqlInsertBuilder Where(params Operator[] operators) => base.Where(operators);
        public new SqlInsertBuilder Returns(IEnumerable<SqlColumn> sqlColumns) => base.Returns(sqlColumns);
    }
}
