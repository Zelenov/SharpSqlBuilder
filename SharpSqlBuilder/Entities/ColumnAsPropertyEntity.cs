using System;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;

namespace SharpSqlBuilder.Entities
{
    /// <summary>
    ///     Represents just a table's column name + as Property part. Used in RETURNING ..., ... block
    ///     <example>column</example>
    /// </summary>
    public class ColumnAsPropertyEntity : SqlBuilderEntity
    {
        public readonly string ColumnName;
        public readonly string PropertyName;

        public ColumnAsPropertyEntity(SqlColumn sqlColumn)
        {
            ColumnName = sqlColumn?.ColumnName ?? throw new ArgumentException(nameof(sqlColumn));
            PropertyName = sqlColumn.PropertyName ?? throw new ArgumentException(nameof(sqlColumn));
        }

        public override bool Present(SqlOptions sqlOptions) => true;

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var columnName = ColumnName;
            var propertyName = PropertyName;
            var command = sqlOptions.Command("AS");
            return $"{columnName} {command} {propertyName}";
        }
    }
}