using SharpSqlBuilder.Operands;
using SharpSqlBuilder.Operators;

namespace SharpSqlBuilder.Maps
{
    public class SqlFilterItem: IOperable
    {
        public string PropertyName { get; set; }
        public Operand AsOperand => new SqlFilterOperand(this);
    }

}