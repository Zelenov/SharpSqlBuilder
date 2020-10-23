using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>DO NOTHING</example>
    /// </summary>
    public class DoNothingBlock : SqlBuilderEntity
    {
        public override bool Present(SqlOptions sqlOptions) => true;

        public override string BuildSql(SqlOptions sqlOptions)
        {
            string command = sqlOptions.Command("DO NOTHING");
            return $"{command}";
        }
    }
}