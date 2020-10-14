using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... LIKE ...</example>
    /// </summary>
    public class LikeOperator : BinaryOperator
    {
        public override int GetPriority(SqlOptions options)
        {
            switch (options.DatabaseType)
            {
                case SqlDatabaseType.Postgres: return 15;
                default: return 7;
            }
        }
        public LikeOperator(Operand leftOperand, Operand rightOperand) : base(leftOperand, rightOperand)
        {
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var left = LeftOperand.BuildSql(sqlOptions, FlowOptions.Construct(this));
            var right = RightOperand.BuildSql(sqlOptions, FlowOptions.Construct(this));
            var command = sqlOptions.Command("LIKE");
            return $"{left} {command} {right}";
        }
    }
}