namespace SharpSqlBuilder.Builders
{
    public abstract class SqlBuilderBase : SqlBuilderEntity
    {
        protected void CheckBeforeBuild(SqlOptions sqlBuilder)
        {
        }

        protected bool CheckBlock(SqlBuilderEntity block, SqlOptions options) => block?.Present(options) == true;
    }
}