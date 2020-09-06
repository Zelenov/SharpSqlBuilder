using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>TRUE</example>
    /// </summary>
    public class TrueOperand : Operand
    {
        public override bool Present(SqlOptions sqlOptions) => true;

        public override string BuildSql(SqlOptions sqlOptions)
        {
            switch (sqlOptions.Database)
            {
                case SqlDatabase.MsSql:
                    return "1";
                default:
                    return sqlOptions.Command("TRUE");
            }
        }
    }
}