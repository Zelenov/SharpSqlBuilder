using System;
using System.Collections.Generic;
using System.Text;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Builders.Delete
{
    public class SqlDeleteBuilder: SqlDeleteBuilderHelper<SqlDeleteBuilder>
    {
        public SqlDeleteBuilder(SqlTable sqlTable) : base(sqlTable)
        {
        }
        public new SqlDeleteBuilder Where(params Operator[] operators) => base.Where(operators);
        public new SqlDeleteBuilder Where(IEnumerable<Operator> operators) => base.Where(operators);
        public new SqlDeleteBuilder Returns(IEnumerable<SqlColumn> sqlColumns) => base.Returns(sqlColumns);
    }
}
