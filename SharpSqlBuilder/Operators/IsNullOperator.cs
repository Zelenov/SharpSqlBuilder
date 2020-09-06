using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... IS NULL</example>
    /// </summary>
    public class IsNullOperator : UnaryOperator
    {
        public IsNullOperator(Operand operand) : base(operand)
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