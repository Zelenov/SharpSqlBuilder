using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... = ...</example>
    /// </summary>
    public class EqualsOneOperator : SimpleBinaryOperator
    {
        public EqualsOneOperator(Operand leftOperand, Operand rightOperand) : base(leftOperand, rightOperand)
        {
        }

        protected override string Command { get; } = "=";
    }
}