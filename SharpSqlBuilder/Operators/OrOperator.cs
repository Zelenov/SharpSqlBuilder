using System.Collections.Generic;
using System.Linq;
using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... OR ... OR ...</example>
    /// </summary>
    public class OrOperator : ChainOperator
    {
        public OrOperator(params Operator[] operators) : base(operators)
        {
        }

        public OrOperator(IEnumerable<Operator> operators) : base(operators)
        {
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var operators = Entities.Select(o => $"{o.BuildSql(sqlOptions)}");
            return string.Join($" {sqlOptions.Command("OR")} ", operators);
        }
    }
}