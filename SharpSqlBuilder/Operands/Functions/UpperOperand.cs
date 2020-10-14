using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>UPPER(...)</example>
    /// </summary>
    public class UpperOperand : FunctionOperand
    {
        public UpperOperand(Operand operand) : base(operand, "UPPER")
        {
        }
    }
}