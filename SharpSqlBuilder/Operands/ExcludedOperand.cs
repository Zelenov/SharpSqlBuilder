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

        public ExcludedOperand(DbMapItem dbMapItem)
        {
            ColumnName = dbMapItem?.ColumnName ?? throw new ArgumentException(nameof(dbMapItem));
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var column = ColumnName;
            string command;
            switch (sqlOptions.Database)
            {
                case SqlDatabase.MySql:
                case SqlDatabase.MariaDb:

                    command = sqlOptions.Command("VALUES");
                    return $"{command}({column})";
                default:
                    command = sqlOptions.Command("EXCLUDED");
                    return $"{command}.{column}";
            }
        }
    }
}