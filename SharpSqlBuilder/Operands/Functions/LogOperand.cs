using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>LOG(...)</example>
    /// </summary>
    public class LogOperand : FunctionOperand
    {
        public LogOperand(Operand operand) : base(operand, "LOG")
        {
        }
    }
}