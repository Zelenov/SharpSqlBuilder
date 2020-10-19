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
        public static class Select
        {
            public static SqlSelectBuilder Values(params SqlTable[] sqlTables) => new SqlSelectBuilder().Values(sqlTables);
            public static SqlSelectBuilder Star() => new SqlSelectBuilder().Star();
            public static SqlSelectBuilder CountStar() => new SqlSelectBuilder().CountStar();
            public static SqlSelectBuilder Value(Operand operand) => new SqlSelectBuilder().Value(operand);
            public static SqlSelectBuilder Values(IEnumerable<Operand> sqlColumns) => new SqlSelectBuilder().Values(sqlColumns);
        }

        public static class Delete
        {
            public static SqlDeleteBuilder From(SqlTable sqlTable) => new SqlDeleteBuilder(sqlTable);
        }
    }
}