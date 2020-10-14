using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>SQUARE(...)</example>
    /// </summary>
    public class SquareOperand : FunctionOperand
    {
        public SquareOperand(Operand operand) : base(operand, "SQUARE")
        {
        }
    }
}