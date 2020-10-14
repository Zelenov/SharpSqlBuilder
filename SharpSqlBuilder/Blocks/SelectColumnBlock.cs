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
    public class SelectColumnBlock : SqlBuilderEntity
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

        public override bool Present(SqlOptions sqlOptions) =>
            TableColumn.Present(sqlOptions) && Value.Present(sqlOptions);

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var tableColumnName = TableColumn.BuildSql(sqlOptions, FlowOptions.Construct(this));
            var property = Value.BuildSql(sqlOptions, FlowOptions.Construct(this));
            var command = sqlOptions.Command("AS");
            return $"{tableColumnName} {command} {property}";
        }
    }
}