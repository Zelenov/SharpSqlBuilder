using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>FALSE</example>
    /// </summary>
    public class FalseOperand : Operand
    {
        public override string BuildSql(SqlOptions sqlOptions)
        {
            switch (sqlOptions.DatabaseType)
            {
                case SqlDatabaseType.MsSql:
                    return "0";
                default:
                    return sqlOptions.Command("FALSE");
            }
        }
    }
}