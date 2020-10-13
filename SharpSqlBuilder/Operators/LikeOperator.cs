using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... LIKE ...</example>
    /// </summary>
    public class LikeOperator : BinaryOperator
    {
        public LikeOperator(Operand leftOperand, Operand rightOperand) : base(leftOperand, rightOperand)
        {
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var left = LeftOperand.BuildSql(sqlOptions);
            var right = RightOperand.BuildSql(sqlOptions);
            var command = sqlOptions.Command("LIKE");
            return $"{left} {command} {right}";
        }
    }
}