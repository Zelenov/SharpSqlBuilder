using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>RADIANS(...)</example>
    /// </summary>
    public class RadiansOperand : FunctionOperand
    {
        public RadiansOperand(Operand operand) : base(operand, "RADIANS")
        {
        }
    }
}