using System;
using System.Collections.Generic;
using System.Text;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operands;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Builders
{
    public class SqlUpdateBuilder: SqlUpdateBuilderHelper<SqlUpdateBuilder>
    {
        public new SqlUpdateBuilder Values(IEnumerable<KeyValuePair<SqlColumn, IOperable>> implicitSets) => base.Values(implicitSets);
        public new SqlUpdateBuilder Values(IEnumerable<SqlColumn> sqlColumns) => base.Values(sqlColumns);
        public new SqlUpdateBuilder Where(params Operator[] operators) => base.Where(operators);
        public new SqlUpdateBuilder Returns(IEnumerable<SqlColumn> sqlColumns) => base.Returns(sqlColumns);

        public SqlUpdateBuilder(SqlTable sqlTable) : base(sqlTable)
        {
        }
    }
}
