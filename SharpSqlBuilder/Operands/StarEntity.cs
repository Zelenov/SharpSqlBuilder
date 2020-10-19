namespace SharpSqlBuilder.Operands
{
    public class StarEntity : SqlBuilderEntity
    {
        public override bool Present(SqlOptions sqlOptions) => true;

        public override string BuildSql(SqlOptions sqlOptions) => "*";
        public static readonly StarEntity Instance = new StarEntity();
    }
}