using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>SIGN(...)</example>
    /// </summary>
    public class SignOperand : FunctionOperand
    {
        public SignOperand(Operand operand) : base(operand, "SIGN")
        {
        }
    }
}