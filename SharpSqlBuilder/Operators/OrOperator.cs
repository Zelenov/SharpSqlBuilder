using System.Collections.Generic;
using System.Linq;
using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... OR ... OR ...</example>
    /// </summary>
    public class OrOperator : ChainOperator
    {
        protected override string Command { get; } = "OR";
        public override int GetPriority(SqlOptions options)
        {
            switch (options.DatabaseType)
            {
                case SqlDatabaseType.Postgres: return 20;
                default: return 7;
            }
        }
        public OrOperator(params Operand[] operators) : base(operators)
        {
        }

        public OrOperator(IEnumerable<Operand> operators) : base(operators)
        {
        }
    }
}