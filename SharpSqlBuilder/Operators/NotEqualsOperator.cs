using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... = ...</example>
    /// </summary>
    public class NotEqualsOperator : EqualityBinaryOperator
    {
        public NotEqualsOperator(Operand leftOperand, Operand rightOperand) : base(leftOperand, rightOperand)
        {
        }

        protected override string Command { get; } = "<>";
    }
}