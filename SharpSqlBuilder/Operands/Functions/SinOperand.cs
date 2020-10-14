using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>SIN(...)</example>
    /// </summary>
    public class SinOperand : FunctionOperand
    {
        public SinOperand(Operand operand) : base(operand, "SIN")
        {
        }
    }
}