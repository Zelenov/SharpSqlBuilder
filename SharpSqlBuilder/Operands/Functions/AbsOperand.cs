using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>ABS(...)</example>
    /// </summary>
    public class AbsOperand : FunctionOperand
    {
        public AbsOperand(Operand operand) : base(operand, "ABS")
        {
        }
    }
}