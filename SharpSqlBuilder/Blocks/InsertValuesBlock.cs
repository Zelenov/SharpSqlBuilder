using System.Linq;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operands;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>VALUES ..., ..., ...</example>
    /// </summary>
    public class InsertValuesBlock : CollectionBlock<Operand>
    {
        public override string BuildSql(SqlOptions sqlOptions)
        {
            var columns = string.Join($",{sqlOptions.NewLine()}{sqlOptions.Indent()}",
                Entities.Select(e => e.BuildSql(sqlOptions, FlowOptions.Construct(this))));
            var command = sqlOptions.Command("VALUES");
            return $"{command} ({sqlOptions.NewLine()}{sqlOptions.Indent()}{columns}{sqlOptions.NewLine()})";
        }
    }
}