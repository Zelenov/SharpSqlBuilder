using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... NOT A</example>
    /// </summary>
    public class NotOperator : UnaryOperator
    {
        public override int GetPriority(SqlOptions options)
        {
            switch (options.DatabaseType)
            {
                case SqlDatabaseType.Postgres: return 18;
                default: return 5;
            }
        }
        public NotOperator(Operand operand) : base(operand)
        {
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var operand = Operand.BuildSql(sqlOptions, FlowOptions.Construct(this));
            var command = sqlOptions.Command("NOT");
            return $"{command} {operand}";
        }
    }
}