using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... = All (...)</example>
    /// </summary>
    public class EqualsAllOperator : BinaryOperator
    {
        public override int GetPriority(SqlOptions options)
        {
            switch (options.DatabaseType)
            {
                case SqlDatabaseType.Postgres: return 11;
                default: return 7;
            }
        }
        public EqualsAllOperator(Operand leftOperand, Operand rightOperand) : base(leftOperand, rightOperand)
        {
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var left = LeftOperand.BuildSql(sqlOptions, FlowOptions.Construct(this));
            var right = RightOperand.BuildSql(sqlOptions, FlowOptions.Construct(this));
            var command = sqlOptions.Command("IN");
            return $"{left} {command} ({right})";
        }
    }
}