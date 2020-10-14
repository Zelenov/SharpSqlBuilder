using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>TRIM(...)</example>
    /// </summary>
    public class TrimOperand : FunctionOperand
    {
        public TrimOperand(Operand operand) : base(operand, "TRIM")
        {
        }
    }
}