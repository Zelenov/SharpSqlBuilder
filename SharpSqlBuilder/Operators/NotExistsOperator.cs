using System;
using SharpSqlBuilder.Builders;
using SharpSqlBuilder.Extensions;

namespace SharpSqlBuilder.Operators
{
    /// <summary>
    /// <example>... NOT EXISTS (SELECT ...)</example>
    /// </summary>
    public class NotExistsOperator : Operator
    {
        private readonly SqlSelectBuilder _selectBuilder;

        public NotExistsOperator(SqlSelectBuilder selectBuilder)
        {
            _selectBuilder = selectBuilder ?? throw new ArgumentException(nameof(selectBuilder));
        }

        public override bool Present(SqlOptions sqlOptions) => _selectBuilder.Present(sqlOptions);

        public override string BuildSql(SqlOptions sqlOptions)
        {
            var operand = _selectBuilder.BuildSql(((SqlOptions)sqlOptions.Clone()).Inlined());
            var command = sqlOptions.Command("NOT EXISTS");
            return $"{command}({operand})";
        }
    }
}