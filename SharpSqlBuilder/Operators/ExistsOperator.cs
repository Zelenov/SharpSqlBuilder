using System;
using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... EXISTS (SELECT ...)</example>
    /// </summary>
    public class ExistsOperator : Operator
    {
        private readonly SqlSelectBuilder _selectBuilder;

        public ExistsOperator(SqlSelectBuilder selectBuilder)
        {
            _selectBuilder = selectBuilder ?? throw new ArgumentException(nameof(selectBuilder));
        }

        public override bool Present(SqlOptions sqlOptions) =>
            _selectBuilder.Present(sqlOptions);

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var operand = _selectBuilder.BuildSql(((SqlOptions)sqlOptions.Clone()).Inlined());
            var command = sqlOptions.Command("EXISTS");
            return $"{command}({operand})";
        }
    }
}