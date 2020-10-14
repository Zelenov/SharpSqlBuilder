using System.Linq;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>SET ... = ..., ... = ...</example>
    /// </summary>
    public class UpdateColumnsBlock : CollectionBlock<UpdateColumnBlock>
    {
        public override string BuildSql(SqlOptions sqlOptions)
        {
            var entities = Entities.Select(c => $"{c.BuildSql(sqlOptions, FlowOptions.Construct(this))}");
            var columns = string.Join($",{sqlOptions.NewLine()}{sqlOptions.Indent()}", entities);
            var command = sqlOptions.Command("SET");
            return $"{command}{sqlOptions.NewLine()}{sqlOptions.Indent()}{columns}";
        }
    }
}