using System.Collections.Generic;
using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder
{
    public static class SqlBuilder
    {
        public static SqlUpdateBuilder Update(SqlTable sqlTable) => new SqlUpdateBuilder(sqlTable);

        public static class Insert
        {
            public static SqlInsertBuilder Into(SqlTable sqlTable) => new SqlInsertBuilder(sqlTable);
        }
        public static SqlSelectBuilder Select => new SqlSelectBuilder();

        public static class Delete
        {
            public static SqlDeleteBuilder From(SqlTable sqlTable) => new SqlDeleteBuilder(sqlTable);
        }
    }
}