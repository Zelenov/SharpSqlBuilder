using SharpSqlBuilder.Entities;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operands;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    ///     SqlColumn AS PropertyName block
    ///     <example>column AS PropertyName</example>
    /// </summary>
    public class SelectColumnBlock : SelectColumnBlockBase
    {
        public readonly Operand TableColumn;
        public readonly OutputValueEntity Value;

        public SelectColumnBlock(SqlColumn sqlColumn)
        {
            TableColumn = Operand.From(sqlColumn);
            Value = new OutputValueEntity(sqlColumn);
        }

        public SelectColumnBlock(Operand operand, OutputValueEntity value)
        {
            TableColumn = operand;
            Value = value;
        }
        public SelectColumnBlock(Operand operand, string @as)
        {
            TableColumn = operand;
            Value = new OutputValueEntity(@as);
        }

        public SelectColumnBlock(Operand operand)
        {
            TableColumn = operand;
        }

        public override bool Present(SqlOptions sqlOptions) =>
            TableColumn.Present(sqlOptions);

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var tableColumnName = TableColumn.BuildSql(sqlOptions, FlowOptions.Construct(this));
            if (Value?.Present(sqlOptions)!=true)
                return $"{tableColumnName}";

            var property = Value.BuildSql(sqlOptions, FlowOptions.Construct(this));
            var command = sqlOptions.Command("AS");
            return $"{tableColumnName} {command} {property}";

        }
    }
}