using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    public abstract class EqualityBinaryOperator : SimpleBinaryOperator
    {
        public override int GetPriority(SqlOptions options)
        {
            switch (options.DatabaseType)
            {
                case SqlDatabaseType.Postgres: return 11;
                default: return 4;
            }
        }
        protected EqualityBinaryOperator(Operand leftOperand, Operand rightOperand) : base(leftOperand, rightOperand)
        {
        }
    }
}