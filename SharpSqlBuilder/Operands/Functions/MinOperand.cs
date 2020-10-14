using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>MIN(...)</example>
    /// </summary>
    public class MinOperand : FunctionOperand
    {
        public MinOperand(Operand operand) : base(operand, "MIN")
        {
        }
    }
}