using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... &lt;= ...</example>
    /// </summary>
    public class LessOrEqualToOperator : EqualityBinaryOperator
    {
        public LessOrEqualToOperator(Operand leftOperand, Operand rightOperand) : base(leftOperand, rightOperand)
        {
        }


        protected override string Command { get; } = "<=";
    }
}