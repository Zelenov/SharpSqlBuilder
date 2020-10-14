using System;
using SharpSqlBuilder.Entities;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operands;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>column = @PropertyName</example>
    /// </summary>
    public class OnConflictUpdateValueBlock : SqlBuilderEntity
    {
        public readonly ColumnEntity ColumnName;
        public readonly Operand Value;

        public OnConflictUpdateValueBlock(SqlColumn sqlColumn)
        {
            ColumnName = new ColumnEntity(sqlColumn ?? throw new ArgumentException(nameof(sqlColumn)));
            Value = sqlColumn.Excluded();
        }

        public override bool Present(SqlOptions sqlOptions) =>
            ColumnName.Present(sqlOptions) && Value.Present(sqlOptions);

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var columnName = ColumnName.BuildSql(sqlOptions, FlowOptions.Construct(this));
            var value = Value.BuildSql(sqlOptions, FlowOptions.Construct(this));
            return $"{columnName} = {value}";
        }
    }
}