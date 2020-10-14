using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>ATN2(...)</example>
    /// </summary>
    public class Atn2Operand : FunctionOperand
    {
        public Atn2Operand(Operand operand) : base(operand, "ATN2")
        {
        }
    }
}