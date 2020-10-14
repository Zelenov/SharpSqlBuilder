using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>ROUND(...)</example>
    /// </summary>
    public class RoundOperand : FunctionOperand
    {
        public RoundOperand(Operand operand) : base(operand, "ROUND")
        {
        }
    }
}