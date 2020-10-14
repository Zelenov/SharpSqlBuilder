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
        public ILikeOperator(Operand leftOperand, Operand rightOperand) : base(leftOperand, rightOperand)
        {
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            switch (sqlOptions.DatabaseType)
            {
                case SqlDatabaseType.Postgres:
                {
                    var left = LeftOperand.BuildSql(sqlOptions);
                    var right = RightOperand.BuildSql(sqlOptions);
                    var command = sqlOptions.Command("ILIKE");
                    return $"{left} {command} {right}";
                }
                default:
                {
                    var left = LeftOperand.Lower().BuildSql(sqlOptions);
                    var right = RightOperand.Lower().BuildSql(sqlOptions);
                    var command = sqlOptions.Command("LIKE");
                    return $"{left} {command} {right}";
                }
            }
        }
    }
}