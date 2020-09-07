using System.Linq;
using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>SELECT ..., ..., ...</example>
    /// </summary>
    public class SelectValuesBlock : CollectionBlock<SelectColumnBlock>
    {
        public override string BuildSql(SqlOptions sqlOptions)
        {
            var columns = string.Join($",{sqlOptions.NewLine()}{sqlOptions.Indent()}", Entities.Select(e => e.BuildSql(sqlOptions)));
            return $"{sqlOptions.Command("SELECT")}{sqlOptions.NewLine()}{sqlOptions.Indent()}{columns}";
        }
    }
}