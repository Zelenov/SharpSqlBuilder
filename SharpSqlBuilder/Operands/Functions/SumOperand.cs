using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>SUM(...)</example>
    /// </summary>
    public class SumOperand : FunctionOperand
    {
        public SumOperand(Operand operand) : base(operand, "SUM")
        {
        }
    }
}