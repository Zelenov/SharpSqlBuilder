using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... &gt; ...</example>
    /// </summary>
    public class MoreThanOperator : EqualityBinaryOperator
    {
        public MoreThanOperator(Operand leftOperand, Operand rightOperand) : base(leftOperand, rightOperand)
        {
        }


        protected override string Command { get; } = ">";
    }
}