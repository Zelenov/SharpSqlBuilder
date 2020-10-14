using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>SQRT(...)</example>
    /// </summary>
    public class SqrtOperand : FunctionOperand
    {
        public SqrtOperand(Operand operand) : base(operand, "SQRT")
        {
        }
    }
}