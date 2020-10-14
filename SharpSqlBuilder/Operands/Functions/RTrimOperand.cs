using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operands
{
    /// <summary>
    /// <example>RTRIM(...)</example>
    /// </summary>
    public class RTrimOperand : FunctionOperand
    {
        public RTrimOperand(Operand operand) : base(operand, "RTRIM")
        {
        }
    }
}