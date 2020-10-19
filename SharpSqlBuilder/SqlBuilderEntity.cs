using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder
{
    public abstract class SqlBuilderEntity
    {
        public abstract bool Present(SqlOptions sqlOptions);
        public abstract string BuildSql(SqlOptions sqlOptions);

        public override string ToString() => BuildSql(SqlOptions.Default ?? new SqlOptions());

        internal virtual string BuildSql(SqlOptions sqlOptions, FlowOptions flowOptions)
        {
            return BuildSql(sqlOptions);
        }
        protected void CheckBeforeBuild(SqlOptions sqlBuilder)
        {
        }
        protected bool CheckBlock(SqlBuilderEntity block, SqlOptions options) => block?.Present(options) == true;
    }
}