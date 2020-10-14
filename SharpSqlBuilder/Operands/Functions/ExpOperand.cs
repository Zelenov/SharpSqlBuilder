using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>EXP(...)</example>
    /// </summary>
    public class ExpOperand : FunctionOperand
    {
        public ExpOperand(Operand operand) : base(operand, "EXP")
        {
        }
    }
}