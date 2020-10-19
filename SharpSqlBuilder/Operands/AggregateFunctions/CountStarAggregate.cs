using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>COUNT(*)</example>
    /// </summary>
    public class CountStarAggregate : Operand
    {
        public readonly StarEntity Entity = StarEntity.Instance;
        
        public override string BuildSql(SqlOptions sqlOptions)
        {
            var entity = Entity;
            var command = sqlOptions.Command("COUNT");
            return $"{command}({entity})";

        }
    }
}