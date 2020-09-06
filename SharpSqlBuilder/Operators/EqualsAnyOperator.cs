using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... = ANY (...)</example>
    /// </summary>
    public class EqualsAnyOperator : BinaryOperator
    {
        public EqualsAnyOperator(Operand leftOperand, Operand rightOperand) : base(leftOperand, rightOperand)
        {
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var left = LeftOperand.BuildSql(sqlOptions);
            var right = RightOperand.BuildSql(sqlOptions);
            switch (sqlOptions.Database)
            {
                case SqlDatabase.Postgres:
                    return $"{left} = {sqlOptions.Command("ANY")}({right})";
                default:
                    return $"{left} {sqlOptions.Command("IN")} ({right})";
            }
        }
    }
}