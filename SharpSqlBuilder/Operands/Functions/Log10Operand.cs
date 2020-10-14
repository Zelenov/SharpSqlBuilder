using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>LOG10(...)</example>
    /// </summary>
    public class Log10Operand : FunctionOperand
    {
        public Log10Operand(Operand operand) : base(operand, "LOG10")
        {
        }
    }
}