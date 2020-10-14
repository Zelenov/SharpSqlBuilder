using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>SELECT *</example>
    /// </summary>
    public class SelectStarBlock : SqlBuilderEntity
    {
        public override bool Present(SqlOptions sqlOptions) => true;

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var command = sqlOptions.Command("SELECT");
            return $"{command} *";
        }
    }
}