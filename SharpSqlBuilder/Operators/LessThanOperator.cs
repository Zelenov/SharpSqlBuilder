using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... &lt; ...</example>
    /// </summary>
    public class LessThanOperator : SimpleBinaryOperator
    {
        public LessThanOperator(Operand leftOperand, Operand rightOperand) : base(leftOperand, rightOperand)
        {
        }


        protected override string Command { get; } = "<";
    }
}