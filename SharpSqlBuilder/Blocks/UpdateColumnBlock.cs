using System;
using SharpSqlBuilder.Entities;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Blocks
{
    /// <summary>
    /// <example>column = @Property</example>
    /// </summary>
    public class UpdateColumnBlock : SqlBuilderEntity
    {
        public ColumnEntity ColumnName;
        public Operand PropertyName;

        public UpdateColumnBlock(DbMapItem dbMapItem)
        {
            ColumnName = new ColumnEntity(dbMapItem ?? throw new ArgumentException(nameof(dbMapItem)));
            PropertyName = dbMapItem.Property();
        }

        public override bool Present(SqlOptions sqlOptions) =>
            ColumnName.Present(sqlOptions) && PropertyName.Present(sqlOptions);

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var columnName = ColumnName.BuildSql(sqlOptions);
            var propertyName = PropertyName.BuildSql(sqlOptions);
            var command = "=";
            return $"{columnName} {command} {propertyName}";
        }
    }
}