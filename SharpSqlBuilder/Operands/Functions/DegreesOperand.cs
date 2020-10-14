using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>DEGREES(...)</example>
    /// </summary>
    public class DegreesOperand : FunctionOperand
    {
        public DegreesOperand(Operand operand) : base(operand, "DEGREES")
        {
        }
    }
}