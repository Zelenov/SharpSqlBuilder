using System.Collections.Generic;
using System.Linq;
using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Extensions;
using SharpSqlBuilder.Operands;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>AND ... AND ...</example>
    /// </summary>
    public class AndOperator : ChainOperator
    {
        protected override string Command { get; } = "AND";
        
        public AndOperator(params Operand[] operands) : base(operands)
        {
        }

        public AndOperator(IEnumerable<Operand> operators) : base(operators)
        {
        }

        public override int GetPriority(SqlOptions options)
        {
            switch (options.DatabaseType)
            {
                case SqlDatabaseType.Postgres: return 19;
                default: return 6;
            }
        }
    }
}