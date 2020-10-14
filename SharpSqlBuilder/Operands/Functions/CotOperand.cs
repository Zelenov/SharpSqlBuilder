using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>COT(...)</example>
    /// </summary>
    public class CotOperand : FunctionOperand
    {
        public CotOperand(Operand operand) : base(operand, "COT")
        {
        }
    }
}