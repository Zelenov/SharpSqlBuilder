using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>FLOOR(...)</example>
    /// </summary>
    public class FloorOperand : FunctionOperand
    {
        public FloorOperand(Operand operand) : base(operand, "FLOOR")
        {
        }
    }
}