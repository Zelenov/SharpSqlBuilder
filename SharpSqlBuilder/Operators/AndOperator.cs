using System.Collections.Generic;
using System.Linq;
using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>AND ... AND ...</example>
    /// </summary>
    public class AndOperator : ChainOperator
    {
        public AndOperator(params Operator[] operands) : base(operands)
        {
        }

        public AndOperator(IEnumerable<Operator> operators) : base(operators)
        {
        }

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var conditions = Entities.Select(o => $"( {o.BuildSql(sqlOptions)} )");
            return string.Join(sqlOptions.Command(" AND "), conditions);
        }
    }
}