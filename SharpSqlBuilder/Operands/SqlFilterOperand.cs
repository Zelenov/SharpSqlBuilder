using System;
using SharpSqlBuilder.Maps;

namespace SharpSqlBuilder.Operands
{
    public class SqlFilterOperand : Operand
    {
        public readonly string PropertyName;

        public SqlFilterOperand(SqlFilterItem sqlFilterItem)
        {
            PropertyName = sqlFilterItem?.PropertyName ?? throw new ArgumentException(nameof(sqlFilterItem));
        }
        public SqlFilterOperand(SqlColumn sqlColumn)
        {
            PropertyName = sqlColumn?.PropertyName ?? throw new ArgumentException(nameof(sqlColumn));
        }

        public override string BuildSql(SqlOptions sqlOptions) => $"@{PropertyName}";
    }
}