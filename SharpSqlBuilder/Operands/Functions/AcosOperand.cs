using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>ACOS(...)</example>
    /// </summary>
    public class AcosOperand : FunctionOperand
    {
        public AcosOperand(Operand operand) : base(operand, "ACOS")
        {
        }
    }
}