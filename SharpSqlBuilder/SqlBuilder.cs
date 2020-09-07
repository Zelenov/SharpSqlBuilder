using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Maps;

namespace SharpSqlBuilder
{
    public static class SqlBuilder
    {
        public static SqlSelectBuilder Select() => new SqlSelectBuilder();

        public static SqlUpdateBuilder Update(SqlTable sqlTable) => new SqlUpdateBuilder(sqlTable);

        public static class Insert
        {
            public static SqlInsertBuilder Into(SqlTable sqlTable) => new SqlInsertBuilder(sqlTable);
        }

        public static class Delete
        {
            public static SqlDeleteBuilder From(SqlTable sqlTable) => new SqlDeleteBuilder(sqlTable);
        }
    }
}