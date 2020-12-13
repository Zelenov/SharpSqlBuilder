using System;
using SharpSqlBuilder.Maps;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Operands
{
    public class SqlFilterOperand : Operand
    {
        public readonly Operand PropertyName;

        public SqlFilterOperand(SqlFilterItem sqlFilterItem)
        {
            PropertyName =
                From($"@{sqlFilterItem?.PropertyName ?? throw new ArgumentException(nameof(sqlFilterItem))}");
        }
        public SqlFilterOperand(SqlColumn sqlColumn)
        {
            if (sqlColumn == null)
                throw new ArgumentException(nameof(sqlColumn));
            var hasCast = sqlColumn.SqlType != null;
            PropertyName = !hasCast? (Operand)From($"@{sqlColumn.PropertyName}"): new CastOperator(From($"@{sqlColumn.PropertyName}"), sqlColumn.SqlType);
        }

        public override string BuildSql(SqlOptions sqlOptions) => $"{PropertyName.BuildSql(sqlOptions)}";
    }
}