namespace SharpSqlBuilder.Blocks
{
    public class SqlCustomSqlBlock : SqlBuilderEntity
    {
        public readonly string CustomSql;

        public SqlCustomSqlBlock(string customSql)
        {
            CustomSql = customSql;
        }

        public override bool Present(SqlOptions sqlOptions) => true;

        public override string BuildSql(SqlOptions sqlOptions) => CustomSql;
    }
}