using System;
using SharpSqlBuilder.Maps;

namespace SharpSqlBuilder.Entities
{
    /// <summary>
    ///     Represents just a table's column name, without (tablename.) part. Used in ON CONFLICT ..., ... block
    ///     <example>column</example>
    /// </summary>
    public class ColumnEntity : SqlBuilderEntity
    {
        public readonly string ColumnName;

        public ColumnEntity(SqlColumn sqlColumn)
        {
            ColumnName = sqlColumn?.ColumnName ?? throw new ArgumentException(nameof(sqlColumn));
        }
        public ColumnEntity(string sqlColumn)
        {
            ColumnName = sqlColumn ?? throw new ArgumentException(nameof(sqlColumn));
        }

        public override bool Present(SqlOptions sqlOptions) => true;

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var columnName = ColumnName;
            return columnName;
        }
    }
}