using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>ASIN(...)</example>
    /// </summary>
    public class AsinOperand : FunctionOperand
    {
        public AsinOperand(Operand operand) : base(operand, "ASIN")
        {
        }
    }
}