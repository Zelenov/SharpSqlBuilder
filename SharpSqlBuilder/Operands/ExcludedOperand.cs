using System;
using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Maps;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    ///     Represents excluded column
    ///     <example>EXCLUDED.column</example>
    /// </summary>
    public class ExcludedOperand : Operand
    {
        public readonly string ColumnName;

        public ExcludedOperand(SqlColumn sqlColumn)
        {
            ColumnName = sqlColumn?.ColumnName ?? throw new ArgumentException(nameof(sqlColumn));
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var column = ColumnName;
            string command;
            switch (sqlOptions.DatabaseType)
            {
                case SqlDatabaseType.MySql:
                case SqlDatabaseType.MariaDb:

                    command = sqlOptions.Command("VALUES");
                    return $"{command}({column})";
                default:
                    command = sqlOptions.Command("EXCLUDED");
                    return $"{command}.{column}";
            }
        }
    }
}