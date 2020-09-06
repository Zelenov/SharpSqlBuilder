using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Maps;

namespace SharpSqlBuilder
{
    public static class SqlBuilder
    {
        public static SqlSelectBuilder Select() => new SqlSelectBuilder();

        public static SqlUpdateBuilder Update(DbMap dbMap) => new SqlUpdateBuilder(dbMap);

        public static class Insert
        {
            public static SqlInsertBuilder Into(DbMap dbMap) => new SqlInsertBuilder(dbMap);
        }

        public static class Delete
        {
            public static SqlDeleteBuilder From(DbMap dbMap) => new SqlDeleteBuilder(dbMap);
        }
    }
}