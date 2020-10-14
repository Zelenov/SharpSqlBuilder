using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>TAN(...)</example>
    /// </summary>
    public class TanOperand : FunctionOperand
    {
        public TanOperand(Operand operand) : base(operand, "TAN")
        {
        }
    }
}