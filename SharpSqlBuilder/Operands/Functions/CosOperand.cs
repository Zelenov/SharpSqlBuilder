using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>COS(...)</example>
    /// </summary>
    public class CosOperand : FunctionOperand
    {
        public CosOperand(Operand operand) : base(operand, "COS")
        {
        }
    }
}