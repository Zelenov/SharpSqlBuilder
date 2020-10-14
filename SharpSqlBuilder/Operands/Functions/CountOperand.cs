using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>COUNT(...)</example>
    /// </summary>
    public class CountOperand : FunctionOperand
    {
        public CountOperand(Operand operand) : base(operand, "COUNT")
        {
        }
    }
}