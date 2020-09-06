namespace SharpSqlBuilder
{
    public abstract class SqlBuilderEntity
    {
        public abstract bool Present(SqlOptions sqlOptions);
        public abstract string BuildSql(SqlOptions sqlOptions);

        public override string ToString() => BuildSql(SqlOptions.Default ?? new SqlOptions());
    }
}