using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... IS NOT NULL</example>
    /// </summary>
    public class NotNullOperator : UnaryOperator
    {
        public NotNullOperator(Operand operand) : base(operand)
        {
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var operand = Operand.BuildSql(sqlOptions);
            var command = sqlOptions.Command("IS NULL");
            return $"{operand} {command}";
        }
    }
}