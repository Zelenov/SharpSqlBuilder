using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>AVG(...)</example>
    /// </summary>
    public class AvgOperand : FunctionOperand
    {
        public AvgOperand(Operand operand) : base(operand, "AVG")
        {
        }
    }
}