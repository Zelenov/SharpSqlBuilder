using System;
using SharpSqlBuilder.Entities;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operands;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>column = @Property</example>
    /// </summary>
    public class UpdateColumnBlock : SqlBuilderEntity
    {
        public ColumnEntity ColumnName;
        public Operand PropertyName;

        public UpdateColumnBlock(SqlColumn sqlColumn, IOperable operand = null)
        {
            ColumnName = new ColumnEntity(sqlColumn ?? throw new ArgumentException(nameof(sqlColumn)));
            PropertyName = operand?.AsOperand??sqlColumn.Property();
        }

        public override bool Present(SqlOptions sqlOptions) =>
            ColumnName.Present(sqlOptions) && PropertyName.Present(sqlOptions);

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var columnName = ColumnName.BuildSql(sqlOptions, FlowOptions.Construct(this));
            var propertyName = PropertyName.BuildSql(sqlOptions, FlowOptions.Construct(this));
            var command = "=";
            return $"{columnName} {command} {propertyName}";
        }
    }
}