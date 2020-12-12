using System.Collections.Generic;
using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Builders.Delete;
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
    public static class SqlBuilder<T>
    {
        public static SqlUpdateBuilder Update(SqlTable sqlTable) => new SqlUpdateBuilder(sqlTable);

        public static class Insert
        {
            public static SqlInsertBuilder<T> Into() => new SqlInsertBuilder<T>();
        }
        public static SqlSelectBuilder<T> Select => new SqlSelectBuilder<T>();

        public static class Delete
        {
            public static SqlDeleteBuilder<T> From() => new SqlDeleteBuilder<T>();
        }
    }

    public static class SqlBuilder<T1, T2>
    {
        public static SqlSelectBuilder<T1, T2> Select() => new SqlSelectBuilder<T1, T2>();
    }
    public static class SqlBuilder<T1, T2, T3>
    {
        public static SqlSelectBuilder<T1, T2, T3> Select() => new SqlSelectBuilder<T1, T2, T3>();
    }
    public static class SqlBuilder<T1, T2, T3, T4>
    {
        public static SqlSelectBuilder<T1, T2, T3, T4> Select() => new SqlSelectBuilder<T1, T2, T3, T4>();
    }
}