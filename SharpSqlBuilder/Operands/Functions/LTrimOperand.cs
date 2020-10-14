using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>LTRIM(...)</example>
    /// </summary>
    public class LTrimOperand : FunctionOperand
    {
        public LTrimOperand(Operand operand) : base(operand, "LTRIM")
        {
        }
    }
}