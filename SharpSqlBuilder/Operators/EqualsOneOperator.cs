using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... = ...</example>
    /// </summary>
    public class EqualsOneOperator : EqualityBinaryOperator
    {
        public override int GetPriority(SqlOptions options)
        {
            switch (options.DatabaseType)
            {
                case SqlDatabaseType.Postgres: return 17;
                default: return 4;
            }
        }
        public EqualsOneOperator(Operand leftOperand, Operand rightOperand) : base(leftOperand, rightOperand)
        {
        }

        protected override string Command { get; } = "=";
    }
}