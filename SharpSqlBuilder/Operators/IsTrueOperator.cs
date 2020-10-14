using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... = TRUE</example>
    /// </summary>
    public class IsTrueOperator : UnaryOperator
    {
        public override int GetPriority(SqlOptions options)
        {
            switch (options.DatabaseType)
            {
                case SqlDatabaseType.Postgres: return 17;
                default: return 4;
            }
        }
        public IsTrueOperator(Operand operand) : base(operand)
        {
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var operand = Operand.BuildSql(sqlOptions, FlowOptions.Construct(this));
            var command = new TrueOperand().BuildSql(sqlOptions, FlowOptions.Construct(this));
            return $"{operand} = {command}";
        }
    }
}