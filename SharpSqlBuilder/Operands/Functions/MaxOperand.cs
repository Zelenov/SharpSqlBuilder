using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>MAX(...)</example>
    /// </summary>
    public class MaxOperand : FunctionOperand
    {
        public MaxOperand(Operand operand) : base(operand, "MAX")
        {
        }
    }
}