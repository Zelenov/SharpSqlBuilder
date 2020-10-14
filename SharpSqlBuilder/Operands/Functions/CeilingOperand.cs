using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>CEILING(...)</example>
    /// </summary>
    public class CeilingOperand : FunctionOperand
    {
        public CeilingOperand(Operand operand) : base(operand, "CEILING")
        {
        }
    }
}