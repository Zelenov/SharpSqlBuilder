using System.Linq;
using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>JOIN ..., JOIN ..., JOIN ...</example>
    /// </summary>
    public class JoinsBlock : CollectionBlock<JoinBlock>
    {
        public override string BuildSql(SqlOptions sqlOptions)
        {
            var joins = Entities.Select(e => e.BuildSql(sqlOptions));
            return string.Join(sqlOptions.NewLine(), joins);
        }
    }
}