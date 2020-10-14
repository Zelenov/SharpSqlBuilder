using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>ATAN(...)</example>
    /// </summary>
    public class AtanOperand : FunctionOperand
    {
        public AtanOperand(Operand operand) : base(operand, "ATAN")
        {
        }
    }
}