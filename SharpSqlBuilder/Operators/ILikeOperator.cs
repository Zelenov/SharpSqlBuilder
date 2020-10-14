using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... ILIKE ...</example>
    /// </summary>
    public class ILikeOperator : BinaryOperator
    {
        public override int GetPriority(SqlOptions options)
        {
            switch (options.DatabaseType)
            {
                case SqlDatabaseType.Postgres: return 15;
                default: return 7;
            }
        }
        public ILikeOperator(Operand leftOperand, Operand rightOperand) : base(leftOperand, rightOperand)
        {
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            switch (sqlOptions.DatabaseType)
            {
                case SqlDatabaseType.Postgres:
                {
                    var left = LeftOperand.BuildSql(sqlOptions, FlowOptions.Construct(this));
                    var right = RightOperand.BuildSql(sqlOptions, FlowOptions.Construct(this));
                    var command = sqlOptions.Command("ILIKE");
                    return $"{left} {command} {right}";
                }
                default:
                {
                    return new LikeOperator(LeftOperand.Lower(), RightOperand.Lower()).BuildSql(sqlOptions, FlowOptions.Construct(this));
                }
            }
        }
    }
}