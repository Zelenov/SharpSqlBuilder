using System.Linq;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>WHERE ..., ..., ...</example>
    /// </summary>
    public class WhereBlock : CollectionBlock<Operator>
    {
        public override string BuildSql(SqlOptions sqlOptions)
        {
            var andOperators = Entities.Count > 1
                ? Entities.Select(c => $"({c.BuildSql(sqlOptions)})")
                : Entities.Select(c => c.BuildSql(sqlOptions));
            var conditions = string.Join($"{sqlOptions.NewLine()}{sqlOptions.Indent()} AND ", andOperators);
            var command = sqlOptions.Command("WHERE");
            return $"{command}{sqlOptions.NewLine()}{sqlOptions.Indent("     ")}{conditions}";
        }
    }
}