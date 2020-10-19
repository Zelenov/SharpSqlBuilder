using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    ///     SqlColumn AS PropertyName block
    ///     <example>column AS PropertyName</example>
    /// </summary>
    public class SelectStarBlock : SelectColumnBlockBase
    {
        public StarEntity Entity = StarEntity.Instance;
        public override bool Present(SqlOptions sqlOptions) => Entity.Present(sqlOptions);

        public override string BuildSql(SqlOptions sqlOptions) => Entity.BuildSql(sqlOptions);
    }
}