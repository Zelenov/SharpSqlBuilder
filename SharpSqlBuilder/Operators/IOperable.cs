using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    public interface IOperable
    {
        Operand AsOperand { get; }
    }
}