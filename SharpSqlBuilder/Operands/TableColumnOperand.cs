using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    ///     TableName + ColumnName Pair
    ///     <example>table.column</example>
    /// </summary>
    public class TableColumnOperand : Operand
    {
        public readonly string ColumnName;
        public readonly string TableName;

        public TableColumnOperand(SqlColumn sqlColumn)
        {
            TableName = sqlColumn.Parent.TableName;
            ColumnName = sqlColumn.ColumnName;
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var tableName = sqlOptions.TableName(TableName);
            var columnName = sqlOptions.ColumnName(ColumnName);
            return $"{tableName}.{columnName}";
        }
    }
}