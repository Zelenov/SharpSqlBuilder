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
            var andBlock = new AndOperator(Entities);
            andBlock.Indent = true;
            andBlock.NewLine = true;
            var conditions = andBlock.BuildSql(sqlOptions, FlowOptions.Construct(this));
            var command = sqlOptions.Command("WHERE");
            return $"{command}{sqlOptions.NewLine()}{sqlOptions.Indent("    ")}{conditions}";
        }
    }
}